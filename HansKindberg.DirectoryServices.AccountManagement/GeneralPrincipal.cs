using System;
using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public class GeneralPrincipal : Principal
	{
		#region Constructors

		public GeneralPrincipal(PrincipalContext context)
		{
			if(context == null)
				throw new ArgumentNullException("context");

			this.ContextRaw = context;
		}

		#endregion
	}
}