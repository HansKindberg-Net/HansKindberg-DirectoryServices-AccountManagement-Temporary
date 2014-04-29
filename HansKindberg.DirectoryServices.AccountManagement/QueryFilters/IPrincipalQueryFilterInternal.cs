namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public interface IPrincipalQueryFilterInternal<in T> where T : IPrincipal
	{
		#region Methods

		void TransferQueryFilter(T queryFilter);

		#endregion
	}
}