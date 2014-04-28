using System.Diagnostics.CodeAnalysis;
using HansKindberg.DirectoryServices.AccountManagement.QueryFilters;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	[SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces")]
	public interface IComputerPrincipalRepository : IPrincipalRepository<IComputerPrincipal, IComputerPrincipalQueryFilter> {}
}