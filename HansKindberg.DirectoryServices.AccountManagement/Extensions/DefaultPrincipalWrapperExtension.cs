using System;
using System.DirectoryServices.AccountManagement;
using System.Globalization;

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

			throw new InvalidCastException(string.Format(CultureInfo.InvariantCulture, "The principal is of type \"{0}\". This is not expected behavior.", principal.GetType()));
		}

		#endregion
	}
}