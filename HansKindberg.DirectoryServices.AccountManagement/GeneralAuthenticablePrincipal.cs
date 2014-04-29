using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public class GeneralAuthenticablePrincipal : AuthenticablePrincipal
	{
		#region Constructors

		public GeneralAuthenticablePrincipal(PrincipalContext context) : base(context) {}
		public GeneralAuthenticablePrincipal(PrincipalContext context, string samAccountName, string password, bool enabled) : base(context, samAccountName, password, enabled) {}

		#endregion
	}
}