using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public interface IGroupPrincipalQueryFilter : IPrincipalQueryFilter
	{
		#region Properties

		GroupScope? GroupScope { get; set; }
		bool? IsSecurityGroup { get; set; }
		ICollection<IPrincipal> Members { get; }

		#endregion
	}
}