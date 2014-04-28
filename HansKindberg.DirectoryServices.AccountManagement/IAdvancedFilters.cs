using System;
using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IAdvancedFilters
	{
		#region Methods

		void AccountExpirationDate(DateTime expirationTime, MatchType match);
		void AccountLockoutTime(DateTime lockoutTime, MatchType match);
		void BadLogOnCount(int logOnCount, MatchType match);
		void LastBadPasswordAttempt(DateTime lastAttempt, MatchType match);
		void LastLogOnTime(DateTime logOnTime, MatchType match);
		void LastPasswordSetTime(DateTime passwordSetTime, MatchType match);

		#endregion
	}
}