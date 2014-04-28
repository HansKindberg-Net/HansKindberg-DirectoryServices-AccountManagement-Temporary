using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IAuthenticablePrincipal : IPrincipal
	{
		#region Properties

		DateTime? AccountExpirationDate { get; set; }
		DateTime? AccountLockoutTime { get; }
		IAdvancedFilters AdvancedSearchFilter { get; }
		bool AllowReversiblePasswordEncryption { get; set; }
		int BadLogOnCount { get; }
		IEnumerable<X509Certificate2> Certificates { get; }
		bool DelegationPermitted { get; set; }
		bool? Enabled { get; set; }
		string HomeDirectory { get; set; }
		string HomeDrive { get; set; }
		DateTime? LastBadPasswordAttempt { get; }
		DateTime? LastLogOn { get; }
		DateTime? LastPasswordSet { get; }
		bool PasswordNeverExpires { get; set; }
		bool PasswordNotRequired { get; set; }
		IEnumerable<byte> PermittedLogOnTimes { get; set; }
		IList<string> PermittedWorkstations { get; }
		string ScriptPath { get; set; }
		bool SmartcardLogOnRequired { get; set; }
		bool UserCannotChangePassword { get; set; }

		#endregion

		#region Methods

		void ChangePassword(string oldPassword, string newPassword);
		void ExpirePasswordNow();
		bool IsAccountLockedOut();
		void RefreshExpiredPassword();
		void SetPassword(string newPassword);
		void UnlockAccount();

		#endregion
	}
}