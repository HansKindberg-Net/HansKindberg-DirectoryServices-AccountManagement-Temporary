using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public class Match<T> : IMatch<T>
	{
		#region Properties

		public virtual MatchType MatchType { get; set; }
		public virtual T Value { get; set; }

		#endregion
	}
}