namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public class QueryFilterValue<T> : IQueryFilterValue<T>
	{
		#region Fields

		private bool _isSet;
		private T _value;

		#endregion

		#region Properties

		public virtual bool IsSet
		{
			get { return this._isSet; }
		}

		public virtual T Value
		{
			get { return this._value; }
			set
			{
				this._isSet = true;
				this._value = value;
			}
		}

		#endregion
	}
}