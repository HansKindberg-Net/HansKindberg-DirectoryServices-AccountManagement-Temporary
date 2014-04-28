namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public abstract class PrincipalQueryFilter : IPrincipalQueryFilter
	{
		#region Properties

		public virtual string Description { get; set; }
		public virtual string DisplayName { get; set; }
		public virtual string Name { get; set; }
		public virtual string SamAccountName { get; set; }
		public virtual string UserPrincipalName { get; set; }

		#endregion
	}
}