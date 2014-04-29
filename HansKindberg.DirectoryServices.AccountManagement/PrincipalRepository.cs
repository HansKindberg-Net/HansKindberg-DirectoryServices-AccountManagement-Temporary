using System;
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

		public PrincipalRepository(IPrincipalConnection principalConnection) : base(principalConnection) {}

		#endregion
	}

	public abstract class PrincipalRepository<T> : IPrincipalRepository<T> where T : IPrincipal
	{
		#region Fields

		private int _pageSize = int.MaxValue;
		private readonly IPrincipalConnection _principalConnection;

		#endregion

		#region Constructors

		protected PrincipalRepository(IPrincipalConnection principalConnection)
		{
			if(principalConnection == null)
				throw new ArgumentNullException("principalConnection");

			this._principalConnection = principalConnection;
		}

		#endregion

		#region Properties

		public virtual int PageSize
		{
			get { return this._pageSize; }
			set { this._pageSize = value; }
		}

		protected internal virtual IPrincipalConnection PrincipalConnection
		{
			get { return this._principalConnection; }
		}

		public virtual int? SizeLimit { get; set; }

		#endregion

		#region Methods

		public virtual T Create()
		{
			return this.Create(false);
		}

		protected internal virtual T Create(bool allowGeneral)
		{
			var abstractPrincipal = (T) this.Wrap(this.CreateConcretePrincipal(allowGeneral));

			var abstractPrincipalInternal = (IPrincipalInternal) abstractPrincipal;

			abstractPrincipalInternal.DisposeContextOnDispose = true;

			return abstractPrincipal;
		}

		protected internal virtual Principal CreateConcretePrincipal(bool allowGeneral)
		{
			Type abstractPrincipalType = typeof(T);
			Type concretePrincipalType = null;

			if(typeof(IComputerPrincipal).IsAssignableFrom(abstractPrincipalType))
				concretePrincipalType = typeof(ComputerPrincipal);
			else if(typeof(IGroupPrincipal).IsAssignableFrom(abstractPrincipalType))
				concretePrincipalType = typeof(GroupPrincipal);
			else if(typeof(IUserPrincipal).IsAssignableFrom(abstractPrincipalType))
				concretePrincipalType = typeof(UserPrincipal);

			if(concretePrincipalType == null && allowGeneral)
			{
				if(typeof(IAuthenticablePrincipal).IsAssignableFrom(abstractPrincipalType))
					concretePrincipalType = typeof(GeneralAuthenticablePrincipal);
				else if(typeof(IPrincipal).IsAssignableFrom(abstractPrincipalType))
					concretePrincipalType = typeof(GeneralPrincipal);
			}

			if(concretePrincipalType == null)
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Could not create a principal of type \"{0}\". The type has no concrete type mapped to it.", abstractPrincipalType));

			Principal concretePrincipal;

			try
			{
				concretePrincipal = (Principal) Activator.CreateInstance(concretePrincipalType, new object[] {this.CreateConcretePrincipalContext()});
			}
			catch(Exception exception)
			{
				throw new TargetInvocationException(string.Format(CultureInfo.InvariantCulture, "A principal of type \"{0}\" could not be created.", concretePrincipalType), exception);
			}

			return concretePrincipal;
		}

		protected internal virtual PrincipalContext CreateConcretePrincipalContext()
		{
			return this.GetConcretePrincipalContext(this.PrincipalConnection.CreatePrincipalContext(), true);
		}

		protected internal virtual IPrincipalInternal CreateQueryFilter(T queryFilter)
		{
			if(Equals(queryFilter, null))
				throw new ArgumentNullException("queryFilter");

			var principalQueryFilterInternal = queryFilter as IPrincipalQueryFilterInternal<T>;

			if(principalQueryFilterInternal != null)
			{
				var concretePrincipal = this.CreateConcretePrincipal(true);

				var queryFilterInternal = (IPrincipalInternal) this.Wrap(concretePrincipal);

				principalQueryFilterInternal.TransferQueryFilter((T) queryFilterInternal);

				return queryFilterInternal;
			}

			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Could not create a query-filter from the type \"{0}\".", queryFilter.GetType()));
		}

		public virtual void Delete(T principal)
		{
			this.GetPrincipal(principal).Delete();
		}

		public virtual IDisposableEnumerable<T> Find(T queryFilter)
		{
			if(Equals(queryFilter, null))
				throw new ArgumentNullException("queryFilter");

			bool disposeQueryFilter = false;
			var queryFilterInternal = queryFilter as IPrincipalInternal;

			try
			{
				if(queryFilterInternal == null)
				{
					queryFilterInternal = this.CreateQueryFilter(queryFilter);
					disposeQueryFilter = true;
				}

				using(var principalSearcher = new PrincipalSearcher())
				{
					principalSearcher.QueryFilter = queryFilterInternal.BasicPrincipal;

					this.ResolveDirectorySearcherSettingsIfNecessary(principalSearcher, queryFilterInternal.Context);

					using(var searchResult = principalSearcher.FindAll())
					{
						return new PrincipalSearchResult<T>(searchResult.Select(principal => (T) this.Wrap(principal)), queryFilterInternal.Context, disposeQueryFilter);
					}
				}
			}
			finally
			{
				if(disposeQueryFilter && queryFilterInternal != null)
					queryFilterInternal.Dispose();
			}
		}

		protected internal virtual PrincipalContext GetConcretePrincipalContext(IPrincipalContext principalContext)
		{
			return this.GetConcretePrincipalContext(principalContext, true);
		}

		protected internal virtual PrincipalContext GetConcretePrincipalContext(IPrincipalContext principalContext, bool throwExceptionIfUnsuccessful)
		{
			if(principalContext == null)
				return null;

			var principalContextInternal = principalContext as IPrincipalContextInternal;

			if(principalContextInternal != null)
				return principalContextInternal.PrincipalContext;

			if(throwExceptionIfUnsuccessful)
				throw new NotImplementedException(string.Format(CultureInfo.InvariantCulture, "The object of type \"{0}\" does not implement \"{1}\".", principalContext.GetType(), typeof(IPrincipalContextInternal)));

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

		public virtual void Save(T principal)
		{
			this.GetPrincipal(principal).Save();
		}

		#endregion
	}
}