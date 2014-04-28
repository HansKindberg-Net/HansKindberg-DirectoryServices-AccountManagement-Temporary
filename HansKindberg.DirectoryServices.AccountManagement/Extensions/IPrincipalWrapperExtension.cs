using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.Extensions
{
	public interface IPrincipalWrapperExtension
	{
		#region Methods

		IPrincipal Wrap(Principal principal);

		#endregion
	}
}