using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public class UserPrincipalWrapper : AuthenticablePrincipalWrapper<UserPrincipal>, IUserPrincipal
	{
		#region Constructors

		public UserPrincipalWrapper(UserPrincipal userPrincipal) : base(userPrincipal, "userPrincipal") {}

		#endregion

		#region Properties

		public virtual IEnumerable<IGroupPrincipal> AuthorizationGroups
		{
			get
			{
				using(var authorizationGroups = this.Principal.GetAuthorizationGroups())
				{
					return authorizationGroups.Cast<GroupPrincipal>().Select(groupPrincipal => (IGroupPrincipal) (GroupPrincipalWrapper) groupPrincipal);
				}
			}
		}

		public virtual string EmailAddress
		{
			get { return this.Principal.EmailAddress; }
			set { this.Principal.EmailAddress = value; }
		}

		public virtual string EmployeeId
		{
			get { return this.Principal.EmployeeId; }
			set { this.Principal.EmployeeId = value; }
		}

		public virtual string GivenName
		{
			get { return this.Principal.GivenName; }
			set { this.Principal.GivenName = value; }
		}

		public virtual string MiddleName
		{
			get { return this.Principal.MiddleName; }
			set { this.Principal.MiddleName = value; }
		}

		public virtual string Surname
		{
			get { return this.Principal.Surname; }
			set { this.Principal.Surname = value; }
		}

		public virtual string VoiceTelephoneNumber
		{
			get { return this.Principal.VoiceTelephoneNumber; }
			set { this.Principal.VoiceTelephoneNumber = value; }
		}

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
}