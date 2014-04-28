using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using HansKindberg.DirectoryServices.AccountManagement.QueryFilters;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public class UserPrincipalRepository : UserPrincipalRepository<UserPrincipal, IUserPrincipal, IUserPrincipalQueryFilter>, IUserPrincipalRepository
	{
		#region Constructors

		public UserPrincipalRepository(IPrincipalContext principalContext) : base(principalContext) {}

		#endregion
	}

	[SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
	public abstract class UserPrincipalRepository<T, TInterface, TQueryFilter> : AuthenticablePrincipalRepository<T, TInterface, TQueryFilter> where T : UserPrincipal where TInterface : IUserPrincipal where TQueryFilter : class, IUserPrincipalQueryFilter
	{
		#region Constructors

		protected UserPrincipalRepository(IPrincipalContext principalContext) : base(principalContext) {}

		#endregion

		#region Methods

		protected internal override IEnumerable<TInterface> CastSearchResult(System.DirectoryServices.AccountManagement.PrincipalSearchResult<Principal> concreteSearchResult)
		{
			if(concreteSearchResult == null)
				throw new ArgumentNullException("concreteSearchResult");

			return concreteSearchResult.Select(item => (TInterface) (IUserPrincipal) (UserPrincipalWrapper) item);
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller. ")]
		protected internal override T CreateConcreteQueryFilter(TQueryFilter queryFilter)
		{
			var userPrincipal = (T) new UserPrincipal(this.CreateConcretePrincipalContext());

			this.PopulateConcreteQueryFilter(userPrincipal, queryFilter);

			return userPrincipal;
		}

		protected internal override void PopulateConcreteQueryFilter(T concreteQueryFilter, TQueryFilter queryFilter)
		{
			base.PopulateConcreteQueryFilter(concreteQueryFilter, queryFilter);

			concreteQueryFilter.EmailAddress = queryFilter.EmailAddress;
			concreteQueryFilter.EmployeeId = queryFilter.EmployeeId;
			concreteQueryFilter.GivenName = queryFilter.GivenName;
			concreteQueryFilter.MiddleName = queryFilter.MiddleName;
			concreteQueryFilter.Surname = queryFilter.Surname;
			concreteQueryFilter.VoiceTelephoneNumber = queryFilter.VoiceTelephoneNumber;
		}

		#endregion
	}
}