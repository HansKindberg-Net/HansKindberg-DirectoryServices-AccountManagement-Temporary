using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.Extensions
{
	public interface IPrincipalExtension
	{
		#region Methods

		Principal GetPrincipal(IPrincipal principal);
		Principal GetPrincipal(IPrincipal principal, bool throwExceptionIfUnsuccessful);
		T GetPrincipal<T>(IPrincipal principal) where T : Principal;
		T GetPrincipal<T>(IPrincipal principal, bool throwExceptionIfUnsuccessful) where T : Principal;

		#endregion
	}
}