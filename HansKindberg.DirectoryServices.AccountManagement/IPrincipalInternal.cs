using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IPrincipalInternal : IPrincipal
	{
		#region Properties

		bool DisposeContextOnDispose { get; set; }
		Principal Principal { get; }

		#endregion
	}
}