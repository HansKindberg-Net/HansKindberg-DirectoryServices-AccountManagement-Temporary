using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public class ComputerPrincipalWrapper : ComputerPrincipalWrapper<ComputerPrincipal>
	{
		#region Constructors

		public ComputerPrincipalWrapper(ComputerPrincipal computerPrincipal) : base(computerPrincipal, "computerPrincipal") {}

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

	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public abstract class ComputerPrincipalWrapper<T> : AuthenticablePrincipalWrapper<T>, IEditablePrincipalInternal, IComputerPrincipal where T : ComputerPrincipal
	{
		#region Constructors

		protected ComputerPrincipalWrapper(T computerPrincipal, string parameterName) : base(computerPrincipal, parameterName) {}

		#endregion

		#region Properties

		public virtual IList<string> ServicePrincipalNames
		{
			get { return this.TypedPrincipal.ServicePrincipalNames; }
		}

		#endregion
	}
}