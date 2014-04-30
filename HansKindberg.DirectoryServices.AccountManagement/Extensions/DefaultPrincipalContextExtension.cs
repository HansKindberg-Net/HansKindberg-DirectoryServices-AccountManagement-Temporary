using System;
using System.DirectoryServices.AccountManagement;
using System.Globalization;

namespace HansKindberg.DirectoryServices.AccountManagement.Extensions
{
	public class DefaultPrincipalContextExtension : IPrincipalContextExtension
	{
		#region Methods

		public virtual PrincipalContext GetPrincipalContext(IPrincipalContext principalContext)
		{
			return this.GetPrincipalContext(principalContext, true);
		}

		public virtual PrincipalContext GetPrincipalContext(IPrincipalContext principalContext, bool throwExceptionIfUnsuccessful)
		{
			if(principalContext == null)
				return null;

			var principalContextInternal = principalContext as IPrincipalContextInternal;

			if(principalContextInternal != null)
				return principalContextInternal.PrincipalContext;

			if(throwExceptionIfUnsuccessful)
				throw new NotImplementedException(string.Format(CultureInfo.InvariantCulture, "The object of type \"{0}\" does not implement \"{1}\".", principalContext.GetType(), typeof(IPrincipalContextInternal)));

			return null;
		}

		#endregion
	}
}