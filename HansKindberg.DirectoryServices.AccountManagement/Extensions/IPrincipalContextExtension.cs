using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.Extensions
{
	public interface IPrincipalContextExtension
	{
		#region Methods

		PrincipalContext GetPrincipalContext(IPrincipalContext principalContext);
		PrincipalContext GetPrincipalContext(IPrincipalContext principalContext, bool throwExceptionIfUnsuccessful);

		#endregion
	}
}