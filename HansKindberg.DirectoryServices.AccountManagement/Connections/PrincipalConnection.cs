using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.Connections
{
	public class PrincipalConnection : IPrincipalContext
	{
		#region Properties

		public virtual string Container { get; set; }
		public virtual ContextType ContextType { get; set; }
		public virtual string Name { get; set; }
		public virtual ContextOptions Options { get; set; }
		public virtual string Password { get; set; }
		public virtual string UserName { get; set; }

		#endregion
	}
}