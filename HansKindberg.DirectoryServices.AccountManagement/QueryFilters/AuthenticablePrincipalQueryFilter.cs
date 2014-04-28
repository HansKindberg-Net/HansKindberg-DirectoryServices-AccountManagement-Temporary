using System;
using System.Collections.Generic;

namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public abstract class AuthenticablePrincipalQueryFilter : PrincipalQueryFilter, IAuthenticablePrincipalQueryFilter
	{
		#region Fields

		private readonly IList<string> _permittedWorkstations = new List<string>();

		#endregion

		#region Properties

		public virtual IMatch<DateTime> AccountExpirationDate { get; set; }
		public virtual IMatch<DateTime> AccountLockoutTime { get; set; }
		public virtual bool AllowReversiblePasswordEncryption { get; set; }
		public virtual IMatch<int> BadLogOnCount { get; set; }
		public virtual bool DelegationPermitted { get; set; }
		public virtual bool? Enabled { get; set; }
		public virtual string HomeDirectory { get; set; }
		public virtual string HomeDrive { get; set; }
		public virtual IMatch<DateTime> LastBadPasswordAttempt { get; set; }
		public virtual IMatch<DateTime> LastLogOn { get; set; }
		public virtual IMatch<DateTime> LastPasswordSet { get; set; }
		public virtual bool PasswordNeverExpires { get; set; }
		public virtual bool PasswordNotRequired { get; set; }
		public virtual IEnumerable<byte> PermittedLogOnTimes { get; set; }

		public virtual IList<string> PermittedWorkstations
		{
			get { return this._permittedWorkstations; }
		}

		public virtual string ScriptPath { get; set; }
		public virtual bool SmartcardLogOnRequired { get; set; }
		public virtual bool UserCannotChangePassword { get; set; }

		#endregion
	}
}