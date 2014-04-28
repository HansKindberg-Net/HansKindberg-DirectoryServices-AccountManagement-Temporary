using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using HansKindberg.DirectoryServices.AccountManagement.Extensions;
using HansKindberg.DirectoryServices.AccountManagement.QueryFilters;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public class GroupPrincipalRepository : GroupPrincipalRepository<GroupPrincipal, IGroupPrincipal, IGroupPrincipalQueryFilter>, IGroupPrincipalRepository
	{
		#region Constructors

		public GroupPrincipalRepository(IPrincipalContext principalContext) : base(principalContext) {}

		#endregion
	}

	[SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
	public abstract class GroupPrincipalRepository<T, TInterface, TQueryFilter> : PrincipalRepository<T, TInterface, TQueryFilter> where T : GroupPrincipal where TInterface : IGroupPrincipal where TQueryFilter : class, IGroupPrincipalQueryFilter
	{
		#region Constructors

		protected GroupPrincipalRepository(IPrincipalContext principalContext) : base(principalContext) {}

		#endregion

		#region Methods

		protected internal override IEnumerable<TInterface> CastSearchResult(System.DirectoryServices.AccountManagement.PrincipalSearchResult<Principal> concreteSearchResult)
		{
			if(concreteSearchResult == null)
				throw new ArgumentNullException("concreteSearchResult");

			return concreteSearchResult.Select(item => (TInterface) (IGroupPrincipal) (GroupPrincipalWrapper) item);
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller. ")]
		protected internal override T CreateConcreteQueryFilter(TQueryFilter queryFilter)
		{
			var groupPrincipal = (T) new GroupPrincipal(this.CreateConcretePrincipalContext());

			this.PopulateConcreteQueryFilter(groupPrincipal, queryFilter);

			return groupPrincipal;
		}

		protected internal override void PopulateConcreteQueryFilter(T concreteQueryFilter, TQueryFilter queryFilter)
		{
			base.PopulateConcreteQueryFilter(concreteQueryFilter, queryFilter);

			concreteQueryFilter.GroupScope = queryFilter.GroupScope;
			concreteQueryFilter.IsSecurityGroup = queryFilter.IsSecurityGroup;

			foreach(var member in queryFilter.Members)
			{
				concreteQueryFilter.Members.Add(this.GetPrincipal(member));
			}
		}

		#endregion
	}
}