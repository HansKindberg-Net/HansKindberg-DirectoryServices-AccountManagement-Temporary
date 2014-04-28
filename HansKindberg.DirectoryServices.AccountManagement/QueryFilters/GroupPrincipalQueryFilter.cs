using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public class GroupPrincipalQueryFilter : PrincipalQueryFilter, IGroupPrincipalQueryFilter
	{
		#region Fields

		private readonly ICollection<IPrincipal> _members = new List<IPrincipal>();

		#endregion

		#region Properties

		public virtual GroupScope? GroupScope { get; set; }
		public virtual bool? IsSecurityGroup { get; set; }

		public virtual ICollection<IPrincipal> Members
		{
			get { return this._members; }
		}

		#endregion
	}
}