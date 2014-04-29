using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.Extensions
{
	public class DefaultPrincipalWrapperExtension : IPrincipalWrapperExtension
	{
		#region Methods

		public virtual IPrincipal Wrap(Principal principal)
		{
			if(principal == null)
				return null;

			var computerPrincipal = principal as ComputerPrincipal;
			if(computerPrincipal != null)
				return (ComputerPrincipalWrapper) computerPrincipal;

			var groupPrincipal = principal as GroupPrincipal;
			if(groupPrincipal != null)
				return (GroupPrincipalWrapper) groupPrincipal;

			var userPrincipal = principal as UserPrincipal;
			if(userPrincipal != null)
				return (UserPrincipalWrapper) userPrincipal;

			var authenticablePrincipal = principal as AuthenticablePrincipal;
			if(authenticablePrincipal != null)
				return (AuthenticablePrincipalWrapper) authenticablePrincipal;

			return (PrincipalWrapper) principal;
		}

		#endregion
	}
}