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
			return this.GetPrincipal(principal, true);
		}

		public virtual Principal GetPrincipal(IPrincipal principal, bool throwExceptionIfUnsuccessful)
		{
			if(principal == null)
				return null;

			var principalInternal = principal as IPrincipalInternal;

			if(principalInternal != null)
				return principalInternal.Principal;

			if(throwExceptionIfUnsuccessful)
				throw new NotImplementedException(string.Format(CultureInfo.InvariantCulture, "The object of type \"{0}\" does not implement \"{1}\".", principal.GetType(), typeof(IPrincipalInternal)));

			return null;
		}

		#endregion
	}
}