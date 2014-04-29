using System;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public class PrincipalContextWrapper : IPrincipalContext, IPrincipalContextInternal
	{
		#region Fields

		private readonly PrincipalContext _principalContext;

		#endregion

		#region Constructors

		public PrincipalContextWrapper(PrincipalContext principalContext)
		{
			if(principalContext == null)
				throw new ArgumentNullException("principalContext");

			this._principalContext = principalContext;
		}

		#endregion

		#region Properties

		public virtual string ConnectedServer
		{
			get { return this.PrincipalContext.ConnectedServer; }
		}

		public virtual string Container
		{
			get { return this.PrincipalContext.Container; }
		}

		public virtual ContextType ContextType
		{
			get { return this.PrincipalContext.ContextType; }
		}

		public virtual string Name
		{
			get { return this.PrincipalContext.Name; }
		}

		public virtual ContextOptions Options
		{
			get { return this.PrincipalContext.Options; }
		}

		public virtual PrincipalContext PrincipalContext
		{
			get { return this._principalContext; }
		}

		public virtual string UserName
		{
			get { return this.PrincipalContext.UserName; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "This is a wrapper.")]
		public virtual void Dispose()
		{
			this.PrincipalContext.Dispose();
		}

		public static PrincipalContextWrapper FromPrincipalContext(PrincipalContext principalContext)
		{
			return principalContext;
		}

		#endregion

		#region Implicit operator

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller. ")]
		public static implicit operator PrincipalContextWrapper(PrincipalContext principalContext)
		{
			return principalContext != null ? new PrincipalContextWrapper(principalContext) : null;
		}

		#endregion
	}
}