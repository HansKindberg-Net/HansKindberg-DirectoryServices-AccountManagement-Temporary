namespace HansKindberg.DirectoryServices.AccountManagement.Connections
{
	public interface IPrincipalConnectionParser
	{
		#region Methods

		IPrincipalConnection Parse(string connectionString);

		#endregion
	}
}