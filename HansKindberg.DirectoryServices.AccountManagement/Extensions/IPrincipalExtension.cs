using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.Extensions
{
	public interface IPrincipalExtension
	{
		#region Methods

		Principal GetPrincipal(IPrincipal principal);
		T GetPrincipal<T>(IPrincipal principal) where T : Principal;

		#endregion
	}
}