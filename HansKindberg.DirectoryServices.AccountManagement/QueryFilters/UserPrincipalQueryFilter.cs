namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public class UserPrincipalQueryFilter : AuthenticablePrincipalQueryFilter, IUserPrincipalQueryFilter
	{
		#region Properties

		public virtual string EmailAddress { get; set; }
		public virtual string EmployeeId { get; set; }
		public virtual string GivenName { get; set; }
		public virtual string MiddleName { get; set; }
		public virtual string Surname { get; set; }
		public virtual string VoiceTelephoneNumber { get; set; }

		#endregion
	}
}