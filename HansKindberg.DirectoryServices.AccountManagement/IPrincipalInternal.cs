using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IPrincipalInternal<out T> where T : Principal
	{
		#region Properties

		T Principal { get; }

		#endregion
	}
}