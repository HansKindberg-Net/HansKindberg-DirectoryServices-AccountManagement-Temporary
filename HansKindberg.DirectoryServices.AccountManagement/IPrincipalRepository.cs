using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;
using HansKindberg.DirectoryServices.AccountManagement.QueryFilters;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IPrincipalRepository<T, in TQueryFilter> where T : IPrincipal where TQueryFilter : class, IPrincipalQueryFilter
	{
		#region Properties

		int PageSize { get; set; }
		int? SizeLimit { get; set; }

		#endregion

		#region Methods

		void Delete(T principal);
		IDisposableEnumerable<T> Find(TQueryFilter queryFilter);
		void Save(T principal);

		#endregion
	}
}