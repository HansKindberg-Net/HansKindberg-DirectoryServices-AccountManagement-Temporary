using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IGroupPrincipal : IEditablePrincipal
	{
		#region Properties

		GroupScope? GroupScope { get; set; }
		bool? IsSecurityGroup { get; set; }

		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
		ICollection<IPrincipal> Members { get; }

		#endregion

		#region Methods

		IDisposableEnumerable<IPrincipal> GetMembers();
		IDisposableEnumerable<IPrincipal> GetMembers(bool recursive);

		#endregion
	}
}