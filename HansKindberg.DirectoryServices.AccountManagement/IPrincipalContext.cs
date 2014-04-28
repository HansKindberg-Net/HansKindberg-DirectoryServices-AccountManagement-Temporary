using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IPrincipalContext
	{
		#region Properties

		string Container { get; }
		ContextType ContextType { get; }
		string Name { get; }
		ContextOptions Options { get; }
		string Password { get; }
		string UserName { get; }

		#endregion
	}
}