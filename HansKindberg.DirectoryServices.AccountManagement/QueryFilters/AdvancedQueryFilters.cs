using System;
using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public class AdvancedQueryFilters : IAdvancedFilters
	{
		#region Properties

		protected internal virtual IMatch<DateTime> AccountExpirationDateValue { get; set; }
		protected internal virtual IMatch<DateTime> AccountLockoutTimeValue { get; set; }
		protected internal virtual IMatch<int> BadLogOnCountValue { get; set; }
		protected internal virtual IMatch<DateTime> LastBadPasswordAttemptValue { get; set; }
		protected internal virtual IMatch<DateTime> LastLogOnTimeValue { get; set; }
		protected internal virtual IMatch<DateTime> LastPasswordSetTimeValue { get; set; }

		#endregion

		#region Methods

		public virtual void AccountExpirationDate(DateTime expirationTime, MatchType match)
		{
			if(this.AccountExpirationDateValue == null)
				this.AccountExpirationDateValue = new Match<DateTime>();

			this.AccountExpirationDateValue.Value = expirationTime;
			this.AccountExpirationDateValue.MatchType = match;
		}

		public virtual void AccountLockoutTime(DateTime lockoutTime, MatchType match)
		{
			if(this.AccountLockoutTimeValue == null)
				this.AccountLockoutTimeValue = new Match<DateTime>();

			this.AccountLockoutTimeValue.Value = lockoutTime;
			this.AccountLockoutTimeValue.MatchType = match;
		}

		public virtual void BadLogOnCount(int logOnCount, MatchType match)
		{
			if(this.BadLogOnCountValue == null)
				this.BadLogOnCountValue = new Match<int>();

			this.BadLogOnCountValue.Value = logOnCount;
			this.BadLogOnCountValue.MatchType = match;
		}

		public virtual void CopyTo(IAdvancedFilters advancedFilters)
		{
			if(advancedFilters == null)
				throw new ArgumentNullException("advancedFilters");

			if(this.AccountExpirationDateValue != null)
				advancedFilters.AccountExpirationDate(this.AccountExpirationDateValue.Value, this.AccountExpirationDateValue.MatchType);

			if(this.AccountLockoutTimeValue != null)
				advancedFilters.AccountLockoutTime(this.AccountLockoutTimeValue.Value, this.AccountLockoutTimeValue.MatchType);

			if(this.BadLogOnCountValue != null)
				advancedFilters.BadLogOnCount(this.BadLogOnCountValue.Value, this.BadLogOnCountValue.MatchType);

			if(this.LastBadPasswordAttemptValue != null)
				advancedFilters.LastBadPasswordAttempt(this.LastBadPasswordAttemptValue.Value, this.LastBadPasswordAttemptValue.MatchType);

			if(this.LastLogOnTimeValue != null)
				advancedFilters.LastLogOnTime(this.LastLogOnTimeValue.Value, this.LastLogOnTimeValue.MatchType);

			if(this.LastPasswordSetTimeValue != null)
				advancedFilters.LastPasswordSetTime(this.LastPasswordSetTimeValue.Value, this.LastPasswordSetTimeValue.MatchType);
		}

		public virtual void LastBadPasswordAttempt(DateTime lastAttempt, MatchType match)
		{
			if(this.LastBadPasswordAttemptValue == null)
				this.LastBadPasswordAttemptValue = new Match<DateTime>();

			this.LastBadPasswordAttemptValue.Value = lastAttempt;
			this.LastBadPasswordAttemptValue.MatchType = match;
		}

		public virtual void LastLogOnTime(DateTime logOnTime, MatchType match)
		{
			if(this.LastLogOnTimeValue == null)
				this.LastLogOnTimeValue = new Match<DateTime>();

			this.LastLogOnTimeValue.Value = logOnTime;
			this.LastLogOnTimeValue.MatchType = match;
		}

		public virtual void LastPasswordSetTime(DateTime passwordSetTime, MatchType match)
		{
			if(this.LastPasswordSetTimeValue == null)
				this.LastPasswordSetTimeValue = new Match<DateTime>();

			this.LastPasswordSetTimeValue.Value = passwordSetTime;
			this.LastPasswordSetTimeValue.MatchType = match;
		}

		public virtual void Reset()
		{
			this.AccountExpirationDateValue = null;
			this.AccountLockoutTimeValue = null;
			this.BadLogOnCountValue = null;
			this.LastBadPasswordAttemptValue = null;
			this.LastLogOnTimeValue = null;
			this.LastPasswordSetTimeValue = null;
		}

		#endregion
	}
}