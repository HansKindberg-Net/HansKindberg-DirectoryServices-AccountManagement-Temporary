using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IGroupPrincipal : IPrincipal
	{
		#region Properties

		GroupScope? GroupScope { get; set; }
		bool? IsSecurityGroup { get; set; }

		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
		ICollection<IPrincipal> Members { get; }

		#endregion

		#region Methods

		IEnumerable<IPrincipal> GetMembers();
		IEnumerable<IPrincipal> GetMembers(bool recursive);

		#endregion
	}
}