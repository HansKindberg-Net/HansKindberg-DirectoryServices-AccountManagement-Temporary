using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IPrincipalContextInternal
	{
		#region Properties

		PrincipalContext PrincipalContext { get; }

		#endregion
	}
}