using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.Extensions
{
	public interface IPrincipalWrapperExtension
	{
		#region Methods

		IPrincipal Wrap(Principal principal);
		TAbstractPrincipal Wrap<TConcretePrincipal, TAbstractPrincipal>(TConcretePrincipal principal) where TConcretePrincipal : Principal where TAbstractPrincipal : IPrincipal;

		#endregion
	}
}