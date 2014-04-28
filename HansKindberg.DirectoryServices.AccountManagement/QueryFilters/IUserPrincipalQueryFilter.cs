namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public interface IUserPrincipalQueryFilter : IAuthenticablePrincipalQueryFilter
	{
		#region Properties

		string EmailAddress { get; set; }
		string EmployeeId { get; set; }
		string GivenName { get; set; }
		string MiddleName { get; set; }
		string Surname { get; set; }
		string VoiceTelephoneNumber { get; set; }

		#endregion
	}
}