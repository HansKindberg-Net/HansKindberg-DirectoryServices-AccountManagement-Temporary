using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using HansKindberg.DirectoryServices.AccountManagement.Extensions;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public class GroupPrincipalWrapper : GroupPrincipalWrapper<GroupPrincipal>
	{
		#region Constructors

		public GroupPrincipalWrapper(GroupPrincipal groupPrincipal) : base(groupPrincipal, "groupPrincipal") {}

		#endregion

		#region Methods

		public static GroupPrincipalWrapper FromGroupPrincipal(GroupPrincipal groupPrincipal)
		{
			return groupPrincipal;
		}

		#endregion

		#region Implicit operator

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller. ")]
		public static implicit operator GroupPrincipalWrapper(GroupPrincipal groupPrincipal)
		{
			return groupPrincipal != null ? new GroupPrincipalWrapper(groupPrincipal) : null;
		}

		#endregion
	}

	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public abstract class GroupPrincipalWrapper<T> : PrincipalWrapper<T>, IEditablePrincipalInternal, IGroupPrincipal where T : GroupPrincipal
	{
		#region Constructors

		protected GroupPrincipalWrapper(T groupPrincipal, string parameterName) : base(groupPrincipal, parameterName) {}

		#endregion

		#region Properties

		public virtual GroupScope? GroupScope
		{
			get { return this.Principal.GroupScope; }
			set { this.Principal.GroupScope = value; }
		}

		public virtual bool? IsSecurityGroup
		{
			get { return this.Principal.IsSecurityGroup; }
			set { this.Principal.IsSecurityGroup = value; }
		}

		public virtual ICollection<IPrincipal> Members
		{
			get { return (PrincipalCollectionWrapper) this.Principal.Members; }
		}

		#endregion

		#region Methods

		public virtual IEnumerable<IPrincipal> GetMembers()
		{
			using(var members = this.Principal.GetMembers())
			{
				return members.Select(this.Wrap);
			}
		}

		public virtual IEnumerable<IPrincipal> GetMembers(bool recursive)
		{
			using(var members = this.Principal.GetMembers(recursive))
			{
				return members.Select(this.Wrap);
			}
		}

		#endregion
	}
}