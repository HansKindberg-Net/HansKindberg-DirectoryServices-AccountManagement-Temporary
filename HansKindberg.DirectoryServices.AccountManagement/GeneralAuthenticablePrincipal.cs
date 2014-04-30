using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	[DirectoryRdnPrefix("CN")]
	[DirectoryObjectClass("*)(|(objectClass=computer)(objectClass=user)")]
	public class GeneralAuthenticablePrincipal : AuthenticablePrincipal
	{
		#region Constructors

		public GeneralAuthenticablePrincipal(PrincipalContext context) : base(context) {}
		public GeneralAuthenticablePrincipal(PrincipalContext context, string samAccountName, string password, bool enabled) : base(context, samAccountName, password, enabled) {}

		#endregion
	}
}