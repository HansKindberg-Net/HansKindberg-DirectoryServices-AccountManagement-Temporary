using System;
using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public class AdvancedFiltersWrapper : IAdvancedFilters
	{
		#region Fields

		private readonly AdvancedFilters _advancedFilters;

		#endregion

		#region Constructors

		public AdvancedFiltersWrapper(AdvancedFilters advancedFilters)
		{
			if(advancedFilters == null)
				throw new ArgumentNullException("advancedFilters");

			this._advancedFilters = advancedFilters;
		}

		#endregion

		#region Properties

		protected internal virtual AdvancedFilters AdvancedFilters
		{
			get { return this._advancedFilters; }
		}

		#endregion

		#region Methods

		public virtual void AccountExpirationDate(DateTime expirationTime, MatchType match)
		{
			this.AdvancedFilters.AccountExpirationDate(expirationTime, match);
		}

		public virtual void AccountLockoutTime(DateTime lockoutTime, MatchType match)
		{
			this.AdvancedFilters.AccountLockoutTime(lockoutTime, match);
		}

		public virtual void BadLogOnCount(int logOnCount, MatchType match)
		{
			this.AdvancedFilters.BadLogonCount(logOnCount, match);
		}

		public static AdvancedFiltersWrapper FromAdvancedFilters(AdvancedFilters advancedFilters)
		{
			return advancedFilters;
		}

		public virtual void LastBadPasswordAttempt(DateTime lastAttempt, MatchType match)
		{
			this.AdvancedFilters.LastBadPasswordAttempt(lastAttempt, match);
		}

		public virtual void LastLogOnTime(DateTime logOnTime, MatchType match)
		{
			this.AdvancedFilters.LastLogonTime(logOnTime, match);
		}

		public virtual void LastPasswordSetTime(DateTime passwordSetTime, MatchType match)
		{
			this.AdvancedFilters.LastPasswordSetTime(passwordSetTime, match);
		}

		#endregion

		#region Implicit operator

		public static implicit operator AdvancedFiltersWrapper(AdvancedFilters advancedFilters)
		{
			return advancedFilters != null ? new AdvancedFiltersWrapper(advancedFilters) : null;
		}

		#endregion
	}
}