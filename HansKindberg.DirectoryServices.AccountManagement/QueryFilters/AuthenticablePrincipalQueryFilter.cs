using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using HansKindberg.DirectoryServices.AccountManagement.Extensions;

namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public class AuthenticablePrincipalQueryFilter : AuthenticablePrincipalQueryFilter<IAuthenticablePrincipal> {}

	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public abstract class AuthenticablePrincipalQueryFilter<T> : PrincipalQueryFilter<T>, IAuthenticablePrincipal where T : IAuthenticablePrincipal
	{
		#region Fields

		private IQueryFilterValue<DateTime?> _accountExpirationDateValue;
		private readonly AdvancedQueryFilters _advancedSearchFilters = new AdvancedQueryFilters();
		private IQueryFilterValue<bool> _allowReversiblePasswordEncryptionValue;
		private IQueryFilterValue<bool> _delegationPermittedValue;
		private IQueryFilterValue<bool?> _enabledValue;
		private IQueryFilterValue<string> _homeDirectoryValue;
		private IQueryFilterValue<string> _homeDriveValue;
		private IQueryFilterValue<bool> _passwordNeverExpiresValue;
		private IQueryFilterValue<bool> _passwordNotRequiredValue;
		private IQueryFilterValue<IEnumerable<byte>> _permittedLogOnTimesValue;
		private readonly IList<string> _permittedWorkstations = new List<string>();
		private IQueryFilterValue<string> _scriptPathValue;
		private IQueryFilterValue<bool> _smartcardLogOnRequiredValue;
		private IQueryFilterValue<bool> _userCannotChangePasswordValue;

		#endregion

		#region Properties

		public virtual DateTime? AccountExpirationDate
		{
			get { return this.AccountExpirationDateValue.Value; }
			set { this.AccountExpirationDateValue.Value = value; }
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		protected internal virtual IQueryFilterValue<DateTime?> AccountExpirationDateValue
		{
			get { return this._accountExpirationDateValue ?? (this._accountExpirationDateValue = new QueryFilterValue<DateTime?>()); }
		}

		public virtual DateTime? AccountLockoutTime
		{
			get { return null; }
		}

		public virtual IAdvancedFilters AdvancedSearchFilter
		{
			get { return this.AdvancedSearchFiltersInternal; }
		}

		protected internal virtual AdvancedQueryFilters AdvancedSearchFiltersInternal
		{
			get { return this._advancedSearchFilters; }
		}

		public virtual bool AllowReversiblePasswordEncryption
		{
			get { return this.AllowReversiblePasswordEncryptionValue.Value; }
			set { this.AllowReversiblePasswordEncryptionValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<bool> AllowReversiblePasswordEncryptionValue
		{
			get { return this._allowReversiblePasswordEncryptionValue ?? (this._allowReversiblePasswordEncryptionValue = new QueryFilterValue<bool>()); }
		}

		public virtual int BadLogOnCount
		{
			get { return 0; }
		}

		public virtual IEnumerable<X509Certificate2> Certificates
		{
			get { return new X509Certificate2[0]; }
		}

		public virtual bool DelegationPermitted
		{
			get { return this.DelegationPermittedValue.Value; }
			set { this.DelegationPermittedValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<bool> DelegationPermittedValue
		{
			get { return this._delegationPermittedValue ?? (this._delegationPermittedValue = new QueryFilterValue<bool>()); }
		}

		public virtual bool? Enabled
		{
			get { return this.EnabledValue.Value; }
			set { this.EnabledValue.Value = value; }
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		protected internal virtual IQueryFilterValue<bool?> EnabledValue
		{
			get { return this._enabledValue ?? (this._enabledValue = new QueryFilterValue<bool?>()); }
		}

		public virtual string HomeDirectory
		{
			get { return this.HomeDirectoryValue.Value; }
			set { this.HomeDirectoryValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<string> HomeDirectoryValue
		{
			get { return this._homeDirectoryValue ?? (this._homeDirectoryValue = new QueryFilterValue<string>()); }
		}

		public virtual string HomeDrive
		{
			get { return this.HomeDriveValue.Value; }
			set { this.HomeDriveValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<string> HomeDriveValue
		{
			get { return this._homeDriveValue ?? (this._homeDriveValue = new QueryFilterValue<string>()); }
		}

		public virtual DateTime? LastBadPasswordAttempt
		{
			get { return null; }
		}

		public virtual DateTime? LastLogOn
		{
			get { return null; }
		}

		public virtual DateTime? LastPasswordSet
		{
			get { return null; }
		}

		public virtual bool PasswordNeverExpires
		{
			get { return this.PasswordNeverExpiresValue.Value; }
			set { this.PasswordNeverExpiresValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<bool> PasswordNeverExpiresValue
		{
			get { return this._passwordNeverExpiresValue ?? (this._passwordNeverExpiresValue = new QueryFilterValue<bool>()); }
		}

		public virtual bool PasswordNotRequired
		{
			get { return this.PasswordNotRequiredValue.Value; }
			set { this.PasswordNotRequiredValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<bool> PasswordNotRequiredValue
		{
			get { return this._passwordNotRequiredValue ?? (this._passwordNotRequiredValue = new QueryFilterValue<bool>()); }
		}

		public virtual IEnumerable<byte> PermittedLogOnTimes
		{
			get { return this.PermittedLogOnTimesValue.Value; }
			set { this.PermittedLogOnTimesValue.Value = value; }
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		protected internal virtual IQueryFilterValue<IEnumerable<byte>> PermittedLogOnTimesValue
		{
			get { return this._permittedLogOnTimesValue ?? (this._permittedLogOnTimesValue = new QueryFilterValue<IEnumerable<byte>>()); }
		}

		public virtual IList<string> PermittedWorkstations
		{
			get { return this._permittedWorkstations; }
		}

		public virtual string ScriptPath
		{
			get { return this.ScriptPathValue.Value; }
			set { this.ScriptPathValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<string> ScriptPathValue
		{
			get { return this._scriptPathValue ?? (this._scriptPathValue = new QueryFilterValue<string>()); }
		}

		public virtual bool SmartcardLogOnRequired
		{
			get { return this.SmartcardLogOnRequiredValue.Value; }
			set { this.SmartcardLogOnRequiredValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<bool> SmartcardLogOnRequiredValue
		{
			get { return this._smartcardLogOnRequiredValue ?? (this._smartcardLogOnRequiredValue = new QueryFilterValue<bool>()); }
		}

		public virtual bool UserCannotChangePassword
		{
			get { return this.UserCannotChangePasswordValue.Value; }
			set { this.UserCannotChangePasswordValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<bool> UserCannotChangePasswordValue
		{
			get { return this._userCannotChangePasswordValue ?? (this._userCannotChangePasswordValue = new QueryFilterValue<bool>()); }
		}

		#endregion

		#region Methods

		public virtual void ChangePassword(string oldPassword, string newPassword) {}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller. ")]
		public override IPrincipal CreateConcreteQueryFilter(IPrincipalContext principalContext)
		{
			T concreteQueryFilter = (T) (object) (AuthenticablePrincipalWrapper) new GeneralAuthenticablePrincipal(this.GetPrincipalContext(principalContext));

			this.TransferQueryFilter(concreteQueryFilter);

			return concreteQueryFilter;
		}

		public virtual void ExpirePasswordNow() {}

		public virtual bool IsAccountLockedOut()
		{
			return false;
		}

		public virtual void RefreshExpiredPassword() {}
		public virtual void SetPassword(string newPassword) {}

		protected internal override void TransferQueryFilter(T queryFilter)
		{
			base.TransferQueryFilter(queryFilter);

			// We skip setting AccountExpirationDate because it is handled with AdvancedSearchFilters.

			if(this.AllowReversiblePasswordEncryptionValue.IsSet)
				queryFilter.AllowReversiblePasswordEncryption = this.AllowReversiblePasswordEncryption;

			if(this.DelegationPermittedValue.IsSet)
				queryFilter.DelegationPermitted = this.DelegationPermitted;

			if(this.EnabledValue.IsSet)
				queryFilter.Enabled = this.Enabled;

			if(this.HomeDirectoryValue.IsSet)
				queryFilter.HomeDirectory = this.HomeDirectory;

			if(this.HomeDriveValue.IsSet)
				queryFilter.HomeDrive = this.HomeDrive;

			if(this.PasswordNeverExpiresValue.IsSet)
				queryFilter.PasswordNeverExpires = this.PasswordNeverExpires;

			if(this.PasswordNotRequiredValue.IsSet)
				queryFilter.PasswordNotRequired = this.PasswordNotRequired;

			if(this.PermittedLogOnTimesValue.IsSet)
				queryFilter.PermittedLogOnTimes = this.PermittedLogOnTimes != null ? this.PermittedLogOnTimes.ToArray() : null;

			if(this.PermittedWorkstations.Any())
			{
				foreach(var permittedWorkstation in this.PermittedWorkstations)
				{
					queryFilter.PermittedWorkstations.Add(permittedWorkstation);
				}
			}

			if(this.ScriptPathValue.IsSet)
				queryFilter.ScriptPath = this.ScriptPath;

			if(this.SmartcardLogOnRequiredValue.IsSet)
				queryFilter.SmartcardLogOnRequired = this.SmartcardLogOnRequired;

			if(this.UserCannotChangePasswordValue.IsSet)
				queryFilter.UserCannotChangePassword = this.UserCannotChangePassword;

			this.AdvancedSearchFiltersInternal.CopyTo(queryFilter.AdvancedSearchFilter);
		}

		public virtual void UnlockAccount() {}

		#endregion
	}
}