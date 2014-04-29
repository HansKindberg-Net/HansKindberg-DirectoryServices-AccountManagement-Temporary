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

		protected AuthenticablePrincipalWrapper(T principal, string parameterName) : base(principal, parameterName) {}

		#endregion

		#region Properties

		public virtual DateTime? AccountExpirationDate
		{
			get { return this.Principal.AccountExpirationDate; }
			set { this.Principal.AccountExpirationDate = value; }
		}

		public virtual DateTime? AccountLockoutTime
		{
			get { return this.Principal.AccountLockoutTime; }
		}

		public virtual IAdvancedFilters AdvancedSearchFilter
		{
			get { return (AdvancedFiltersWrapper) this.Principal.AdvancedSearchFilter; }
		}

		public virtual bool AllowReversiblePasswordEncryption
		{
			get { return this.Principal.AllowReversiblePasswordEncryption; }
			set { this.Principal.AllowReversiblePasswordEncryption = value; }
		}

		public virtual int BadLogOnCount
		{
			get { return this.Principal.BadLogonCount; }
		}

		public virtual IEnumerable<X509Certificate2> Certificates
		{
			get { return this.Principal.Certificates.Cast<X509Certificate2>(); }
		}

		public virtual bool DelegationPermitted
		{
			get { return this.Principal.DelegationPermitted; }
			set { this.Principal.DelegationPermitted = value; }
		}

		public virtual bool? Enabled
		{
			get { return this.Principal.Enabled; }
			set { this.Principal.Enabled = value; }
		}

		public virtual string HomeDirectory
		{
			get { return this.Principal.HomeDirectory; }
			set { this.Principal.HomeDirectory = value; }
		}

		public virtual string HomeDrive
		{
			get { return this.Principal.HomeDrive; }
			set { this.Principal.HomeDrive = value; }
		}

		public virtual DateTime? LastBadPasswordAttempt
		{
			get { return this.Principal.LastBadPasswordAttempt; }
		}

		public virtual DateTime? LastLogOn
		{
			get { return this.Principal.LastLogon; }
		}

		public virtual DateTime? LastPasswordSet
		{
			get { return this.Principal.LastPasswordSet; }
		}

		public virtual bool PasswordNeverExpires
		{
			get { return this.Principal.PasswordNeverExpires; }
			set { this.Principal.PasswordNeverExpires = value; }
		}

		public virtual bool PasswordNotRequired
		{
			get { return this.Principal.PasswordNotRequired; }
			set { this.Principal.PasswordNotRequired = value; }
		}

		public virtual IEnumerable<byte> PermittedLogOnTimes
		{
			get { return this.Principal.PermittedLogonTimes; }
			set { this.Principal.PermittedLogonTimes = value != null ? value.ToArray() : null; }
		}

		public virtual IList<string> PermittedWorkstations
		{
			get { return this.Principal.PermittedWorkstations; }
		}

		public virtual string ScriptPath
		{
			get { return this.Principal.ScriptPath; }
			set { this.Principal.ScriptPath = value; }
		}

		public virtual bool SmartcardLogOnRequired
		{
			get { return this.Principal.SmartcardLogonRequired; }
			set { this.Principal.SmartcardLogonRequired = value; }
		}

		public virtual bool UserCannotChangePassword
		{
			get { return this.Principal.UserCannotChangePassword; }
			set { this.Principal.UserCannotChangePassword = value; }
		}

		#endregion

		#region Methods

		public virtual void ChangePassword(string oldPassword, string newPassword)
		{
			this.Principal.ChangePassword(oldPassword, newPassword);
		}

		public virtual void ExpirePasswordNow()
		{
			this.Principal.ExpirePasswordNow();
		}

		public virtual bool IsAccountLockedOut()
		{
			return this.Principal.IsAccountLockedOut();
		}

		public virtual void RefreshExpiredPassword()
		{
			this.Principal.RefreshExpiredPassword();
		}

		public virtual void SetPassword(string newPassword)
		{
			this.Principal.SetPassword(newPassword);
		}

		public virtual void UnlockAccount()
		{
			this.Principal.UnlockAccount();
		}

		#endregion
	}
}