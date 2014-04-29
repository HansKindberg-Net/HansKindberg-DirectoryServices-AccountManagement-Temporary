using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;
using HansKindberg.DirectoryServices.AccountManagement.Extensions;
using HansKindberg.DirectoryServices.AccountManagement.QueryFilters;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	[SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
	public abstract class PrincipalRepository<T, TInterface, TQueryFilter> : IPrincipalRepository<TInterface, TQueryFilter> where T : Principal where TInterface : IPrincipal where TQueryFilter : class, IPrincipalQueryFilter
	{
		#region Fields

		private int _pageSize = int.MaxValue;
		private readonly IPrincipalContext _principalContext;

		#endregion

		#region Constructors

		protected PrincipalRepository(IPrincipalContext principalContext)
		{
			if(principalContext == null)
				throw new ArgumentNullException("principalContext");

			this._principalContext = principalContext;
		}

		#endregion

		#region Properties

		public virtual int PageSize
		{
			get { return this._pageSize; }
			set { this._pageSize = value; }
		}

		protected internal virtual IPrincipalContext PrincipalContext
		{
			get { return this._principalContext; }
		}

		public virtual int? SizeLimit { get; set; }

		#endregion

		#region Methods

		protected internal abstract IEnumerable<TInterface> CastSearchResult(System.DirectoryServices.AccountManagement.PrincipalSearchResult<Principal> concreteSearchResult);

		protected internal virtual PrincipalContext CreateConcretePrincipalContext()
		{
			return new PrincipalContext(ContextType.Machine);
			//return new PrincipalContext(this.PrincipalContext.ContextType, this.PrincipalContext.Name, this.PrincipalContext.Container, this.PrincipalContext.Options, this.PrincipalContext.UserName, this.PrincipalContext.Password);
		}

		protected internal abstract T CreateConcreteQueryFilter(TQueryFilter queryFilter);

		public virtual void Delete(TInterface principal)
		{
			this.GetPrincipal(principal).Delete();
		}

		public virtual IDisposableEnumerable<TInterface> Find(TQueryFilter queryFilter)
		{
			if(queryFilter == null)
				throw new ArgumentNullException("queryFilter");

			using(var concreteQueryFilter = this.CreateConcreteQueryFilter(queryFilter))
			{
				using(var principalSearcher = new PrincipalSearcher())
				{
					principalSearcher.QueryFilter = concreteQueryFilter;

					using(var searchResult = principalSearcher.FindAll())
					{
						return new PrincipalSearchResult<TInterface>(concreteQueryFilter.Context, this.CastSearchResult(searchResult));
					}
				}
			}
		}

		protected internal virtual void PopulateConcreteQueryFilter(T concreteQueryFilter, TQueryFilter queryFilter)
		{
			if(concreteQueryFilter == null)
				throw new ArgumentNullException("concreteQueryFilter");

			if(queryFilter == null)
				return;

			concreteQueryFilter.Description = queryFilter.Description;
			concreteQueryFilter.DisplayName = queryFilter.DisplayName;
			concreteQueryFilter.Name = queryFilter.Name;
			concreteQueryFilter.SamAccountName = queryFilter.SamAccountName;
			concreteQueryFilter.UserPrincipalName = queryFilter.UserPrincipalName;
		}

		protected internal virtual void ResolveDirectorySearcherSettingsIfNecessary(PrincipalSearcher principalSearcher)
		{
			if(principalSearcher == null)
				throw new ArgumentNullException("principalSearcher");

			if(this.PrincipalContext.ContextType == ContextType.Domain && principalSearcher.GetUnderlyingSearcherType() == typeof(DirectorySearcher))
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

		public virtual void Save(TInterface principal)
		{
			this.GetPrincipal(principal).Save();
		}

		#endregion
	}
}