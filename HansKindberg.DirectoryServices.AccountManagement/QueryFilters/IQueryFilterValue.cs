namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public interface IQueryFilterValue<T>
	{
		#region Properties

		bool IsSet { get; }
		T Value { get; set; }

		#endregion
	}
}