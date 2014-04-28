using System;
using System.Collections.Generic;

namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public interface IAuthenticablePrincipalQueryFilter : IPrincipalQueryFilter
	{
		#region Properties

		IMatch<DateTime> AccountExpirationDate { get; set; }
		IMatch<DateTime> AccountLockoutTime { get; set; }
		bool AllowReversiblePasswordEncryption { get; set; }
		IMatch<int> BadLogOnCount { get; set; }
		bool DelegationPermitted { get; set; }
		bool? Enabled { get; set; }
		string HomeDirectory { get; set; }
		string HomeDrive { get; set; }
		IMatch<DateTime> LastBadPasswordAttempt { get; set; }
		IMatch<DateTime> LastLogOn { get; set; }
		IMatch<DateTime> LastPasswordSet { get; set; }
		bool PasswordNeverExpires { get; set; }
		bool PasswordNotRequired { get; set; }
		IEnumerable<byte> PermittedLogOnTimes { get; set; }
		IList<string> PermittedWorkstations { get; }
		string ScriptPath { get; set; }
		bool SmartcardLogOnRequired { get; set; }
		bool UserCannotChangePassword { get; set; }

		#endregion
	}
}