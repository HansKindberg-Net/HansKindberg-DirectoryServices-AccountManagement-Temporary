namespace HansKindberg.DirectoryServices.AccountManagement.Connections
{
	public interface IPrincipalConnectionParser
	{
		#region Methods

		IPrincipalContext Parse(string connectionString);

		#endregion
	}
}