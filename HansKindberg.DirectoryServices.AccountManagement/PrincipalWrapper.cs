using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using HansKindberg.DirectoryServices.AccountManagement.Extensions;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public class PrincipalWrapper : PrincipalWrapper<Principal>
	{
		#region Constructors

		public PrincipalWrapper(Principal principal) : base(principal, "principal") {}

		#endregion

		#region Methods

		public static PrincipalWrapper FromPrincipal(Principal principal)
		{
			return principal;
		}

		#endregion

		#region Implicit operator

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller. ")]
		public static implicit operator PrincipalWrapper(Principal principal)
		{
			return principal != null ? new PrincipalWrapper(principal) : null;
		}

		#endregion
	}

	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public abstract class PrincipalWrapper<T> : IPrincipalInternal<T> where T : Principal
	{
		#region Fields

		private readonly T _principal;

		#endregion

		#region Constructors

		protected PrincipalWrapper(T principal, string parameterName)
		{
			if(principal == null)
				throw new ArgumentNullException(parameterName ?? string.Empty);

			this._principal = principal;
		}

		#endregion

		#region Properties

		public virtual Principal BasicPrincipal
		{
			get { return this.Principal; }
		}

		public virtual IPrincipalContext Context
		{
			get { return (PrincipalContextWrapper) this.Principal.Context; }
		}

		public virtual ContextType ContextType
		{
			get { return this.Principal.ContextType; }
		}

		public virtual string Description
		{
			get { return this.Principal.Description; }
			set { this.Principal.Description = value; }
		}

		public virtual string DisplayName
		{
			get { return this.Principal.DisplayName; }
			set { this.Principal.DisplayName = value; }
		}

		public virtual bool DisposeContextOnDispose { get; set; }

		public virtual string DistinguishedName
		{
			get { return this.Principal.DistinguishedName; }
		}

		public virtual IEnumerable<IGroupPrincipal> Groups
		{
			get
			{
				using(var groups = this.Principal.GetGroups())
				{
					return groups.Cast<GroupPrincipal>().Select(groupPrincipal => (IGroupPrincipal) (GroupPrincipalWrapper) groupPrincipal);
				}
			}
		}

		public virtual Guid? Guid
		{
			get { return this.Principal.Guid; }
		}

		public virtual string Name
		{
			get { return this.Principal.Name; }
			set { this.Principal.Name = value; }
		}

		public virtual T Principal
		{
			get { return this._principal; }
		}

		public virtual string SamAccountName
		{
			get { return this.Principal.SamAccountName; }
			set { this.Principal.SamAccountName = value; }
		}

		public virtual SecurityIdentifier Sid
		{
			get { return this.Principal.Sid; }
		}

		public virtual string StructuralObjectClass
		{
			get { return this.Principal.StructuralObjectClass; }
		}

		public virtual object UnderlyingObject
		{
			get { return this.Principal.GetUnderlyingObject(); }
		}

		public virtual Type UnderlyingObjectType
		{
			get { return this.Principal.GetUnderlyingObjectType(); }
		}

		public virtual string UserPrincipalName
		{
			get { return this.Principal.UserPrincipalName; }
			set { this.Principal.UserPrincipalName = value; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "This is a wrapper.")]
		public virtual void Dispose()
		{
			if(this.DisposeContextOnDispose)
				this.Principal.Context.Dispose();

			this.Principal.Dispose();
		}

		public override bool Equals(object obj)
		{
			if(obj == null)
				return false;

			if(ReferenceEquals(this, obj))
				return true;

			var principalWrapper = obj as PrincipalWrapper<T>;

			if(principalWrapper != null)
				return this.Principal.Equals(principalWrapper.Principal);

			return false;
		}

		public override int GetHashCode()
		{
			return this.Principal.GetHashCode();
		}

		public virtual bool IsMemberOf(IGroupPrincipal group)
		{
			return this.Principal.IsMemberOf(this.GetPrincipal<GroupPrincipal>(group));
		}

		public virtual bool IsMemberOf(IdentityType identityType, string identityValue)
		{
			return this.Principal.IsMemberOf(this.Principal.Context, identityType, identityValue);
		}

		#endregion
	}
}