namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public interface IPrincipalQueryFilter
	{
		#region Properties

		string Description { get; set; }
		string DisplayName { get; set; }
		string Name { get; set; }
		string SamAccountName { get; set; }
		string UserPrincipalName { get; set; }

		#endregion
	}
}