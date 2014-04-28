using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public class ComputerPrincipalWrapper : AuthenticablePrincipalWrapper<ComputerPrincipal>, IComputerPrincipal
	{
		#region Constructors

		public ComputerPrincipalWrapper(ComputerPrincipal computerPrincipal) : base(computerPrincipal, "computerPrincipal") {}

		#endregion

		#region Properties

		public virtual IList<string> ServicePrincipalNames
		{
			get { return this.Principal.ServicePrincipalNames; }
		}

		#endregion

		#region Methods

		public static ComputerPrincipalWrapper FromComputerPrincipal(ComputerPrincipal computerPrincipal)
		{
			return computerPrincipal;
		}

		#endregion

		#region Implicit operator

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller. ")]
		public static implicit operator ComputerPrincipalWrapper(ComputerPrincipal computerPrincipal)
		{
			return computerPrincipal != null ? new ComputerPrincipalWrapper(computerPrincipal) : null;
		}

		#endregion
	}
}