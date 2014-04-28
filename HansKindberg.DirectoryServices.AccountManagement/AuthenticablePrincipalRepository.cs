using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using HansKindberg.DirectoryServices.AccountManagement.QueryFilters;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	[SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
	public abstract class AuthenticablePrincipalRepository<T, TInterface, TQueryFilter> : PrincipalRepository<T, TInterface, TQueryFilter> where T : AuthenticablePrincipal where TInterface : IAuthenticablePrincipal where TQueryFilter : class, IAuthenticablePrincipalQueryFilter
	{
		#region Constructors

		protected AuthenticablePrincipalRepository(IPrincipalContext principalContext) : base(principalContext) {}

		#endregion

		#region Methods

		protected internal override void PopulateConcreteQueryFilter(T concreteQueryFilter, TQueryFilter queryFilter)
		{
			base.PopulateConcreteQueryFilter(concreteQueryFilter, queryFilter);

			if(queryFilter.AccountExpirationDate != null)
				concreteQueryFilter.AdvancedSearchFilter.AccountExpirationDate(queryFilter.AccountExpirationDate.Value, queryFilter.AccountExpirationDate.MatchType);

			if(queryFilter.AccountLockoutTime != null)
				concreteQueryFilter.AdvancedSearchFilter.AccountLockoutTime(queryFilter.AccountLockoutTime.Value, queryFilter.AccountLockoutTime.MatchType);

			concreteQueryFilter.AllowReversiblePasswordEncryption = queryFilter.AllowReversiblePasswordEncryption;

			if(queryFilter.BadLogOnCount != null)
				concreteQueryFilter.AdvancedSearchFilter.BadLogonCount(queryFilter.BadLogOnCount.Value, queryFilter.BadLogOnCount.MatchType);

			concreteQueryFilter.DelegationPermitted = queryFilter.DelegationPermitted;
			concreteQueryFilter.Enabled = queryFilter.Enabled;
			concreteQueryFilter.HomeDirectory = queryFilter.HomeDirectory;
			concreteQueryFilter.HomeDrive = queryFilter.HomeDrive;

			if(queryFilter.LastBadPasswordAttempt != null)
				concreteQueryFilter.AdvancedSearchFilter.LastBadPasswordAttempt(queryFilter.LastBadPasswordAttempt.Value, queryFilter.LastBadPasswordAttempt.MatchType);

			if(queryFilter.LastLogOn != null)
				concreteQueryFilter.AdvancedSearchFilter.LastLogonTime(queryFilter.LastLogOn.Value, queryFilter.LastLogOn.MatchType);

			if(queryFilter.LastPasswordSet != null)
				concreteQueryFilter.AdvancedSearchFilter.LastPasswordSetTime(queryFilter.LastPasswordSet.Value, queryFilter.LastPasswordSet.MatchType);

			concreteQueryFilter.PasswordNeverExpires = queryFilter.PasswordNeverExpires;
			concreteQueryFilter.PasswordNotRequired = queryFilter.PasswordNotRequired;
			concreteQueryFilter.PermittedLogonTimes = queryFilter.PermittedLogOnTimes.ToArray();

			foreach(var permittedWorkstation in queryFilter.PermittedWorkstations)
			{
				concreteQueryFilter.PermittedWorkstations.Add(permittedWorkstation);
			}

			concreteQueryFilter.ScriptPath = queryFilter.ScriptPath;
			concreteQueryFilter.SmartcardLogonRequired = queryFilter.SmartcardLogOnRequired;
			concreteQueryFilter.UserCannotChangePassword = queryFilter.UserCannotChangePassword;
		}

		#endregion
	}
}