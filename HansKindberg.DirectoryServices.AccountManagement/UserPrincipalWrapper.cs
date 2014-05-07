using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;
using HansKindberg.DirectoryServices.AccountManagement.Extensions;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public class UserPrincipalWrapper : UserPrincipalWrapper<UserPrincipal>
	{
		#region Constructors

		public UserPrincipalWrapper(UserPrincipal userPrincipal) : base(userPrincipal, "userPrincipal") {}

		#endregion

		#region Methods

		public static UserPrincipalWrapper FromUserPrincipal(UserPrincipal userPrincipal)
		{
			return userPrincipal;
		}

		#endregion

		#region Implicit operator

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller. ")]
		public static implicit operator UserPrincipalWrapper(UserPrincipal userPrincipal)
		{
			return userPrincipal != null ? new UserPrincipalWrapper(userPrincipal) : null;
		}

		#endregion
	}

	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public abstract class UserPrincipalWrapper<T> : AuthenticablePrincipalWrapper<T>, IEditablePrincipalInternal, IUserPrincipal where T : UserPrincipal
	{
		#region Constructors

		protected UserPrincipalWrapper(T userPrincipal, string parameterName) : base(userPrincipal, parameterName) {}

		#endregion

		#region Properties

		public virtual IDisposableEnumerable<IGroupPrincipal> AuthorizationGroups
		{
			get { return new DisposableEnumerableWrapper<GroupPrincipal, IGroupPrincipal>(this.TypedPrincipal.GetAuthorizationGroups().Cast<GroupPrincipal>(), this.Wrap<GroupPrincipal, IGroupPrincipal>); }
		}

		public virtual string EmailAddress
		{
			get { return this.TypedPrincipal.EmailAddress; }
			set { this.TypedPrincipal.EmailAddress = value; }
		}

		public virtual string EmployeeId
		{
			get { return this.TypedPrincipal.EmployeeId; }
			set { this.TypedPrincipal.EmployeeId = value; }
		}

		public virtual string GivenName
		{
			get { return this.TypedPrincipal.GivenName; }
			set { this.TypedPrincipal.GivenName = value; }
		}

		public virtual string MiddleName
		{
			get { return this.TypedPrincipal.MiddleName; }
			set { this.TypedPrincipal.MiddleName = value; }
		}

		public virtual string Surname
		{
			get { return this.TypedPrincipal.Surname; }
			set { this.TypedPrincipal.Surname = value; }
		}

		public virtual string VoiceTelephoneNumber
		{
			get { return this.TypedPrincipal.VoiceTelephoneNumber; }
			set { this.TypedPrincipal.VoiceTelephoneNumber = value; }
		}

		#endregion
	}
}