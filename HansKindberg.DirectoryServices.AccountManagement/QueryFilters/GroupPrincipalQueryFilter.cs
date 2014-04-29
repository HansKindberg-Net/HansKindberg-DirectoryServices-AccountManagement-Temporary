using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public class GroupPrincipalQueryFilter : GroupPrincipalQueryFilter<IGroupPrincipal> {}

	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public abstract class GroupPrincipalQueryFilter<T> : PrincipalQueryFilter<T>, IGroupPrincipal where T : IGroupPrincipal
	{
		#region Fields

		private IQueryFilterValue<GroupScope?> _groupScopeValue;
		private IQueryFilterValue<bool?> _isSecurityGroupValue;
		private readonly ICollection<IPrincipal> _members = new List<IPrincipal>().AsReadOnly();

		#endregion

		#region Properties

		public virtual GroupScope? GroupScope
		{
			get { return this.GroupScopeValue.Value; }
			set { this.GroupScopeValue.Value = value; }
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		protected internal virtual IQueryFilterValue<GroupScope?> GroupScopeValue
		{
			get { return this._groupScopeValue ?? (this._groupScopeValue = new QueryFilterValue<GroupScope?>()); }
		}

		public virtual bool? IsSecurityGroup
		{
			get { return this.IsSecurityGroupValue.Value; }
			set { this.IsSecurityGroupValue.Value = value; }
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		protected internal virtual IQueryFilterValue<bool?> IsSecurityGroupValue
		{
			get { return this._isSecurityGroupValue ?? (this._isSecurityGroupValue = new QueryFilterValue<bool?>()); }
		}

		public virtual ICollection<IPrincipal> Members
		{
			get { return this._members; }
		}

		#endregion

		#region Methods

		public virtual IEnumerable<IPrincipal> GetMembers()
		{
			return new IPrincipal[0];
		}

		public virtual IEnumerable<IPrincipal> GetMembers(bool recursive)
		{
			return new IPrincipal[0];
		}

		public override void TransferQueryFilter(T queryFilter)
		{
			base.TransferQueryFilter(queryFilter);

			if(this.GroupScopeValue.IsSet)
				queryFilter.GroupScope = this.GroupScope;

			if(this.IsSecurityGroupValue.IsSet)
				queryFilter.IsSecurityGroup = this.IsSecurityGroup;
		}

		#endregion
	}
}