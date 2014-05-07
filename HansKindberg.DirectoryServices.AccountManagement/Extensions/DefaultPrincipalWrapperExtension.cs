using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.Extensions
{
	public class DefaultPrincipalWrapperExtension : IPrincipalWrapperExtension
	{
		#region Methods

		public virtual IPrincipal Wrap(Principal principal)
		{
			return this.Wrap<Principal, IPrincipal>(principal);
		}

		public virtual TAbstractPrincipal Wrap<TConcretePrincipal, TAbstractPrincipal>(TConcretePrincipal principal) where TConcretePrincipal : Principal where TAbstractPrincipal : IPrincipal
		{
			if(principal == null)
				return default(TAbstractPrincipal);

			var computerPrincipal = principal as ComputerPrincipal;
			if(computerPrincipal != null)
				return (TAbstractPrincipal) (IComputerPrincipal) (ComputerPrincipalWrapper) computerPrincipal;

			var groupPrincipal = principal as GroupPrincipal;
			if(groupPrincipal != null)
				return (TAbstractPrincipal) (IGroupPrincipal) (GroupPrincipalWrapper) groupPrincipal;

			var userPrincipal = principal as UserPrincipal;
			if(userPrincipal != null)
				return (TAbstractPrincipal) (IUserPrincipal) (UserPrincipalWrapper) userPrincipal;

			var authenticablePrincipal = principal as AuthenticablePrincipal;
			if(authenticablePrincipal != null)
				return (TAbstractPrincipal) (IAuthenticablePrincipal) (AuthenticablePrincipalWrapper) authenticablePrincipal;

			return (TAbstractPrincipal) (IPrincipal) (PrincipalWrapper) principal;
		}

		#endregion
	}
}