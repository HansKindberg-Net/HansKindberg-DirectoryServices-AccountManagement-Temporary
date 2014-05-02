using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.Extensions
{
	public interface IPrincipalExtension
	{
		#region Methods

		Principal GetPrincipal(IPrincipal principal);
		Principal GetPrincipal(IPrincipal principal, bool throwExceptionIfUnsuccessful);

		#endregion
	}
}