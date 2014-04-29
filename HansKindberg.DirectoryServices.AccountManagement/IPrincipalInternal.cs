using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IPrincipalInternal : IPrincipal
	{
		#region Properties

		Principal BasicPrincipal { get; }
		bool DisposeContextOnDispose { get; set; }

		#endregion
	}

	public interface IPrincipalInternal<out T> : IPrincipalInternal where T : Principal
	{
		#region Properties

		T Principal { get; }

		#endregion
	}
}