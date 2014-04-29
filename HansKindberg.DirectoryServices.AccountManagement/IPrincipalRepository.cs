using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IPrincipalRepository<T> where T : IPrincipal
	{
		#region Properties

		int PageSize { get; set; }
		int? SizeLimit { get; set; }

		#endregion

		#region Methods

		void Delete(T principal);
		IDisposableEnumerable<T> Find(T queryFilter);
		void Save(T principal);

		#endregion
	}
}