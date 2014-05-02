using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public class AuthenticablePrincipalWrapper : AuthenticablePrincipalWrapper<AuthenticablePrincipal>
	{
		#region Constructors

		public AuthenticablePrincipalWrapper(AuthenticablePrincipal authenticablePrincipal) : base(authenticablePrincipal, "authenticablePrincipal") {}

		#endregion

		#region Methods

		public static AuthenticablePrincipalWrapper FromAuthenticablePrincipal(AuthenticablePrincipal authenticablePrincipal)
		{
			return authenticablePrincipal;
		}

		#endregion

		#region Implicit operator

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller. ")]
		public static implicit operator AuthenticablePrincipalWrapper(AuthenticablePrincipal authenticablePrincipal)
		{
			return authenticablePrincipal != null ? new AuthenticablePrincipalWrapper(authenticablePrincipal) : null;
		}

		#endregion
	}

	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public abstract class AuthenticablePrincipalWrapper<T> : PrincipalWrapper<T>, IAuthenticablePrincipal where T : AuthenticablePrincipal
	{
		#region Constructors

		protected AuthenticablePrincipalWrapper(T authenticablePrincipal, string parameterName) : base(authenticablePrincipal, parameterName) {}

		#endregion

		#region Properties

		public virtual DateTime? AccountExpirationDate
		{
			get { return this.TypedPrincipal.AccountExpirationDate; }
			set { this.TypedPrincipal.AccountExpirationDate = value; }
		}

		public virtual DateTime? AccountLockoutTime
		{
			get { return this.TypedPrincipal.AccountLockoutTime; }
		}

		public virtual IAdvancedFilters AdvancedSearchFilter
		{
			get { return (AdvancedFiltersWrapper) this.TypedPrincipal.AdvancedSearchFilter; }
		}

		public virtual bool AllowReversiblePasswordEncryption
		{
			get { return this.TypedPrincipal.AllowReversiblePasswordEncryption; }
			set { this.TypedPrincipal.AllowReversiblePasswordEncryption = value; }
		}

		public virtual int BadLogOnCount
		{
			get { return this.TypedPrincipal.BadLogonCount; }
		}

		public virtual IEnumerable<X509Certificate2> Certificates
		{
			get { return this.TypedPrincipal.Certificates.Cast<X509Certificate2>(); }
		}

		public virtual bool DelegationPermitted
		{
			get { return this.TypedPrincipal.DelegationPermitted; }
			set { this.TypedPrincipal.DelegationPermitted = value; }
		}

		public virtual bool? Enabled
		{
			get { return this.TypedPrincipal.Enabled; }
			set { this.TypedPrincipal.Enabled = value; }
		}

		public virtual string HomeDirectory
		{
			get { return this.TypedPrincipal.HomeDirectory; }
			set { this.TypedPrincipal.HomeDirectory = value; }
		}

		public virtual string HomeDrive
		{
			get { return this.TypedPrincipal.HomeDrive; }
			set { this.TypedPrincipal.HomeDrive = value; }
		}

		public virtual DateTime? LastBadPasswordAttempt
		{
			get { return this.TypedPrincipal.LastBadPasswordAttempt; }
		}

		public virtual DateTime? LastLogOn
		{
			get { return this.TypedPrincipal.LastLogon; }
		}

		public virtual DateTime? LastPasswordSet
		{
			get { return this.TypedPrincipal.LastPasswordSet; }
		}

		public virtual bool PasswordNeverExpires
		{
			get { return this.TypedPrincipal.PasswordNeverExpires; }
			set { this.TypedPrincipal.PasswordNeverExpires = value; }
		}

		public virtual bool PasswordNotRequired
		{
			get { return this.TypedPrincipal.PasswordNotRequired; }
			set { this.TypedPrincipal.PasswordNotRequired = value; }
		}

		public virtual IEnumerable<byte> PermittedLogOnTimes
		{
			get { return this.TypedPrincipal.PermittedLogonTimes; }
			set { this.TypedPrincipal.PermittedLogonTimes = value != null ? value.ToArray() : null; }
		}

		public virtual IList<string> PermittedWorkstations
		{
			get { return this.TypedPrincipal.PermittedWorkstations; }
		}

		public virtual string ScriptPath
		{
			get { return this.TypedPrincipal.ScriptPath; }
			set { this.TypedPrincipal.ScriptPath = value; }
		}

		public virtual bool SmartcardLogOnRequired
		{
			get { return this.TypedPrincipal.SmartcardLogonRequired; }
			set { this.TypedPrincipal.SmartcardLogonRequired = value; }
		}

		public virtual bool UserCannotChangePassword
		{
			get { return this.TypedPrincipal.UserCannotChangePassword; }
			set { this.TypedPrincipal.UserCannotChangePassword = value; }
		}

		#endregion

		#region Methods

		public virtual void ChangePassword(string oldPassword, string newPassword)
		{
			this.TypedPrincipal.ChangePassword(oldPassword, newPassword);
		}

		public virtual void ExpirePasswordNow()
		{
			this.TypedPrincipal.ExpirePasswordNow();
		}

		public virtual bool IsAccountLockedOut()
		{
			return this.TypedPrincipal.IsAccountLockedOut();
		}

		public virtual void RefreshExpiredPassword()
		{
			this.TypedPrincipal.RefreshExpiredPassword();
		}

		public virtual void SetPassword(string newPassword)
		{
			this.TypedPrincipal.SetPassword(newPassword);
		}

		public virtual void UnlockAccount()
		{
			this.TypedPrincipal.UnlockAccount();
		}

		#endregion
	}
}