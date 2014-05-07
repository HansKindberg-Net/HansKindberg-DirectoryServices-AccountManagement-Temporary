using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;
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
			get { return this.TypedPrincipal.GroupScope; }
			set { this.TypedPrincipal.GroupScope = value; }
		}

		public virtual bool? IsSecurityGroup
		{
			get { return this.TypedPrincipal.IsSecurityGroup; }
			set { this.TypedPrincipal.IsSecurityGroup = value; }
		}

		public virtual ICollection<IPrincipal> Members
		{
			get { return (PrincipalCollectionWrapper) this.TypedPrincipal.Members; }
		}

		#endregion

		#region Methods

		public virtual IDisposableEnumerable<IPrincipal> GetMembers()
		{
			return new DisposableEnumerableWrapper<Principal, IPrincipal>(this.TypedPrincipal.GetMembers(), this.Wrap);
		}

		public virtual IDisposableEnumerable<IPrincipal> GetMembers(bool recursive)
		{
			return new DisposableEnumerableWrapper<Principal, IPrincipal>(this.TypedPrincipal.GetMembers(recursive), this.Wrap);
		}

		#endregion
	}
}