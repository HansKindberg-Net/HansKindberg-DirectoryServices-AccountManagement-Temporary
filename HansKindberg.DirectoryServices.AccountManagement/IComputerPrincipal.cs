using System.Collections.Generic;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IComputerPrincipal : IAuthenticablePrincipal
	{
		#region Properties

		IList<string> ServicePrincipalNames { get; }

		#endregion
	}
}