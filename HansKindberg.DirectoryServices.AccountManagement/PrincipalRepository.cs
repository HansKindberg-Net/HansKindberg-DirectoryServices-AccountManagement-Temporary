﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Linq;
using System.Reflection;
using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;
using HansKindberg.DirectoryServices.AccountManagement.Connections;
using HansKindberg.DirectoryServices.AccountManagement.Extensions;
using HansKindberg.DirectoryServices.AccountManagement.QueryFilters;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public class PrincipalRepository : PrincipalRepository<IPrincipal>
	{
		#region Constructors

		public PrincipalRepository(IPrincipalConnection connection) : base(connection) {}

		#endregion
	}

	public abstract class PrincipalRepository<T> : IPrincipalRepository<T> where T : IPrincipal
	{
		#region Fields

		private readonly IPrincipalConnection _connection;
		private int _pageSize = int.MaxValue;

		#endregion

		#region Constructors

		protected PrincipalRepository(IPrincipalConnection connection)
		{
			if(connection == null)
				throw new ArgumentNullException("connection");

			this._connection = connection;
		}

		#endregion

		#region Properties

		public virtual IPrincipalConnection Connection
		{
			get { return this._connection; }
		}

		public virtual int PageSize
		{
			get { return this._pageSize; }
			set { this._pageSize = value; }
		}

		public virtual int? SizeLimit { get; set; }

		#endregion

		#region Methods

		public virtual TEditablePrincipal Create<TEditablePrincipal>() where TEditablePrincipal : IEditablePrincipal
		{
			var principal = this.CreateInternal<TEditablePrincipal>(this.Connection.CreatePrincipalContext());
			principal.DisposeContextOnDispose = true;

			return (TEditablePrincipal) principal;
		}

		public virtual TEditablePrincipal Create<TEditablePrincipal>(string container) where TEditablePrincipal : IEditablePrincipal
		{
			var principalConnectionCopy = new PrincipalConnection
			{
				Container = container,
				ContextType = this.Connection.ContextType,
				Name = this.Connection.Name,
				Options = this.Connection.Options,
				Password = this.Connection.Password,
				UserName = this.Connection.UserName,
			};

			var principal = this.CreateInternal<TEditablePrincipal>(principalConnectionCopy.CreatePrincipalContext());
			principal.DisposeContextOnDispose = true;

			return (TEditablePrincipal) principal;
		}

		public virtual TEditablePrincipal Create<TEditablePrincipal>(IPrincipalContext principalContext) where TEditablePrincipal : IEditablePrincipal
		{
			var principal = this.CreateInternal<TEditablePrincipal>(principalContext);

			return (TEditablePrincipal) principal;
		}

		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		protected internal virtual IEditablePrincipalInternal CreateInternal<TEditablePrincipal>(IPrincipalContext principalContext) where TEditablePrincipal : IEditablePrincipal
		{
			var principalTypeToCreate = this.GetConcretePrincipalTypeToCreate<TEditablePrincipal>();

			if(principalTypeToCreate == null)
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "An object of type \"{0}\" can not be created. The type is not mapped to any concrete type.", typeof(TEditablePrincipal)));

			Principal principal;

			try
			{
				principal = (Principal) Activator.CreateInstance(principalTypeToCreate, new object[] {this.GetPrincipalContext(principalContext)});
			}
			catch(Exception exception)
			{
				throw new TargetInvocationException(string.Format(CultureInfo.InvariantCulture, "A principal of type \"{0}\" could not be created.", principalTypeToCreate), exception);
			}

			IEditablePrincipalInternal editablePrincipal;

			try
			{
				editablePrincipal = (IEditablePrincipalInternal) this.Wrap(principal);
			}
			catch(Exception exception)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The principal of type \"{0}\" could not be wrapped.", principalTypeToCreate), exception);
			}

			return editablePrincipal;
		}

		public virtual void Delete(IEditablePrincipal principal)
		{
			if(principal == null)
				throw new ArgumentNullException("principal");

			this.GetPrincipal(principal).Delete();
		}

		public virtual IDisposableEnumerable<T> Find(T queryFilter)
		{
			if(Equals(queryFilter, null))
				throw new ArgumentNullException("queryFilter");

			var concreteQueryFilter = queryFilter as IPrincipalInternal;
			bool disposeConcreteQueryFilter = concreteQueryFilter == null;

			try
			{
				if(concreteQueryFilter == null)
				{
					var queryFilterInternal = queryFilter as IPrincipalQueryFilterInternal;

					if(queryFilterInternal == null)
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "In the current implementation the query-filter must be of type \"{0}\" or \"{1}\".", typeof(IPrincipalInternal), typeof(IPrincipalQueryFilterInternal)));

					concreteQueryFilter = (IPrincipalInternal) queryFilterInternal.CreateConcreteQueryFilter(this.Connection.CreatePrincipalContext());
				}

				using(var principalSearcher = new PrincipalSearcher())
				{
					principalSearcher.QueryFilter = concreteQueryFilter.Principal;

					this.ResolveDirectorySearcherSettingsIfNecessary(principalSearcher, concreteQueryFilter.Context);

					using(var searchResult = principalSearcher.FindAll())
					{
						return new PrincipalSearchResult<T>(searchResult.Select(principal => (T) this.Wrap(principal)), concreteQueryFilter.Context, disposeConcreteQueryFilter);
					}
				}
			}
			catch
			{
				if(disposeConcreteQueryFilter && concreteQueryFilter != null && concreteQueryFilter.Context != null)
					concreteQueryFilter.Context.Dispose();

				throw;
			}
			finally
			{
				if(disposeConcreteQueryFilter && concreteQueryFilter != null)
					concreteQueryFilter.Dispose();
			}
		}

		public virtual T Get(string identity)
		{
			return this.Get<T>(identity);
		}

		public virtual T Get(string identity, IdentityType identityType)
		{
			return this.Get<T>(identity, identityType);
		}

		public virtual TPrincipal Get<TPrincipal>(string identity) where TPrincipal : IPrincipal
		{
			return this.Get<TPrincipal>(identity, null);
		}

		public virtual TPrincipal Get<TPrincipal>(string identity, IdentityType identityType) where TPrincipal : IPrincipal
		{
			return this.Get<TPrincipal>(identity, (IdentityType?) identityType);
		}

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		protected internal virtual TPrincipal Get<TPrincipal>(string identity, IdentityType? identityType) where TPrincipal : IPrincipal
		{
			var principalContext = this.Connection.CreatePrincipalContext();

			try
			{
				var principal = !identityType.HasValue ? GeneralPrincipal.Get(this.GetPrincipalContext(principalContext), this.GetConcretePrincipalType<TPrincipal>(), identity) : GeneralPrincipal.Get(this.GetPrincipalContext(principalContext), this.GetConcretePrincipalType<TPrincipal>(), identityType.Value, identity);

				if(principal == null)
				{
					principalContext.Dispose();
					return default(TPrincipal);
				}

				var principalInternal = (IPrincipalInternal) this.Wrap(principal);

				principalInternal.DisposeContextOnDispose = true;

				return (TPrincipal) principalInternal;
			}
			catch
			{
				principalContext.Dispose();

				throw;
			}
		}

		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		protected internal virtual Type GetConcretePrincipalType<TAbstractPrincipal>() where TAbstractPrincipal : IPrincipal
		{
			var abstractPrincipalType = typeof(TAbstractPrincipal);

			if(typeof(IComputerPrincipal).IsAssignableFrom(abstractPrincipalType))
				return typeof(ComputerPrincipal);

			if(typeof(IGroupPrincipal).IsAssignableFrom(abstractPrincipalType))
				return typeof(GroupPrincipal);

			if(typeof(IUserPrincipal).IsAssignableFrom(abstractPrincipalType))
				return typeof(UserPrincipal);

			if(typeof(IAuthenticablePrincipal).IsAssignableFrom(abstractPrincipalType))
				return typeof(AuthenticablePrincipal);

			return typeof(Principal);
		}

		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		protected internal virtual Type GetConcretePrincipalTypeToCreate<TEditablePrincipal>() where TEditablePrincipal : IEditablePrincipal
		{
			var editablePrincipalType = typeof(TEditablePrincipal);

			if(typeof(IComputerPrincipal).IsAssignableFrom(editablePrincipalType))
				return typeof(ComputerPrincipal);

			if(typeof(IGroupPrincipal).IsAssignableFrom(editablePrincipalType))
				return typeof(GroupPrincipal);

			if(typeof(IUserPrincipal).IsAssignableFrom(editablePrincipalType))
				return typeof(UserPrincipal);

			return null;
		}

		protected internal virtual void ResolveDirectorySearcherSettingsIfNecessary(PrincipalSearcher principalSearcher, IPrincipalContext principalContext)
		{
			if(principalSearcher == null)
				throw new ArgumentNullException("principalSearcher");

			if(principalContext == null)
				throw new ArgumentNullException("principalContext");

			if(principalContext.ContextType == ContextType.Domain && principalSearcher.GetUnderlyingSearcherType() == typeof(DirectorySearcher))
			{
				var directorySearcher = (DirectorySearcher) principalSearcher.GetUnderlyingSearcher();
				//directorySearcher.Sort.PropertyName = this.GetIdentityType().ToString();
				//directorySearcher.Sort.Direction = SortDirection.Ascending;

				if(this.SizeLimit.HasValue)
					directorySearcher.SizeLimit = this.SizeLimit.Value;
				else
					directorySearcher.PageSize = this.PageSize;
			}
		}

		public virtual void Save(IEditablePrincipal principal)
		{
			if(principal == null)
				throw new ArgumentNullException("principal");

			this.GetPrincipal(principal).Save();
		}

		public virtual void Save(IEditablePrincipal principal, IPrincipalContext principalContext)
		{
			if(principal == null)
				throw new ArgumentNullException("principal");

			if(principalContext == null)
				throw new ArgumentNullException("principalContext");

			this.GetPrincipal(principal).Save(this.GetPrincipalContext(principalContext));
		}

		#endregion
	}
}