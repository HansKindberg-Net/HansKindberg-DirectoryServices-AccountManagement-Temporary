using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public interface IMatch<T>
	{
		#region Properties

		MatchType MatchType { get; set; }
		T Value { get; set; }

		#endregion
	}
}