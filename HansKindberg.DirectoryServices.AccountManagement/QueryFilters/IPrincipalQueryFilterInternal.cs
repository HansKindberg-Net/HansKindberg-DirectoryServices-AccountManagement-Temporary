namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public interface IPrincipalQueryFilterInternal
	{
		#region Methods

		IPrincipal CreateConcreteQueryFilter(IPrincipalContext principalContext);

		#endregion
	}
}