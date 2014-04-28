using System.Collections.Generic;

namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public interface IComputerPrincipalQueryFilter : IAuthenticablePrincipalQueryFilter
	{
		#region Properties

		IList<string> ServicePrincipalNames { get; }

		#endregion
	}
}