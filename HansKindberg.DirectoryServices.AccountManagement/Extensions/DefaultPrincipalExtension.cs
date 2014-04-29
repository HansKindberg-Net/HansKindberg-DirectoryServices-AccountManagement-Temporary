using System;
using System.DirectoryServices.AccountManagement;
using System.Globalization;

namespace HansKindberg.DirectoryServices.AccountManagement.Extensions
{
	public class DefaultPrincipalExtension : IPrincipalExtension
	{
		#region Methods

		public virtual Principal GetPrincipal(IPrincipal principal)
		{
			return this.GetPrincipal<Principal>(principal);
		}

		public virtual Principal GetPrincipal(IPrincipal principal, bool throwExceptionIfUnsuccessful)
		{
			return this.GetPrincipal<Principal>(principal, throwExceptionIfUnsuccessful);
		}

		public virtual T GetPrincipal<T>(IPrincipal principal) where T : Principal
		{
			return this.GetPrincipal<T>(principal, true);
		}

		public virtual T GetPrincipal<T>(IPrincipal principal, bool throwExceptionIfUnsuccessful) where T : Principal
		{
			if(principal == null)
				return null;

			var principalInternal = principal as IPrincipalInternal<T>;

			if(principalInternal != null)
				return principalInternal.Principal;

			if(throwExceptionIfUnsuccessful)
				throw new NotImplementedException(string.Format(CultureInfo.InvariantCulture, "The object of type \"{0}\" does not implement \"{1}\".", principal.GetType(), typeof(IPrincipalInternal<>)));

			return null;
		}

		#endregion
	}
}