using System;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;
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
	public abstract class PrincipalWrapper<T> : IPrincipalInternal where T : Principal
	{
		#region Fields

		private readonly T _typedPrincipal;

		#endregion

		#region Constructors

		protected PrincipalWrapper(T principal, string parameterName)
		{
			if(principal == null)
				throw new ArgumentNullException(parameterName ?? string.Empty);

			this._typedPrincipal = principal;
		}

		#endregion

		#region Properties

		public virtual IPrincipalContext Context
		{
			get { return (PrincipalContextWrapper) this.TypedPrincipal.Context; }
		}

		public virtual ContextType ContextType
		{
			get { return this.TypedPrincipal.ContextType; }
		}

		public virtual string Description
		{
			get { return this.TypedPrincipal.Description; }
			set { this.TypedPrincipal.Description = value; }
		}

		public virtual string DisplayName
		{
			get { return this.TypedPrincipal.DisplayName; }
			set { this.TypedPrincipal.DisplayName = value; }
		}

		public virtual bool DisposeContextOnDispose { get; set; }

		public virtual string DistinguishedName
		{
			get { return this.TypedPrincipal.DistinguishedName; }
		}

		public virtual IDisposableEnumerable<IGroupPrincipal> Groups
		{
			get { return new DisposableEnumerableWrapper<GroupPrincipal, IGroupPrincipal>(this.TypedPrincipal.GetGroups().Cast<GroupPrincipal>(), this.Wrap<GroupPrincipal, IGroupPrincipal>); }
		}

		public virtual Guid? Guid
		{
			get { return this.TypedPrincipal.Guid; }
		}

		public virtual string Name
		{
			get { return this.TypedPrincipal.Name; }
			set { this.TypedPrincipal.Name = value; }
		}

		public virtual Principal Principal
		{
			get { return this.TypedPrincipal; }
		}

		public virtual string SamAccountName
		{
			get { return this.TypedPrincipal.SamAccountName; }
			set { this.TypedPrincipal.SamAccountName = value; }
		}

		public virtual SecurityIdentifier Sid
		{
			get { return this.TypedPrincipal.Sid; }
		}

		public virtual string StructuralObjectClass
		{
			get { return this.TypedPrincipal.StructuralObjectClass; }
		}

		public virtual T TypedPrincipal
		{
			get { return this._typedPrincipal; }
		}

		public virtual object UnderlyingObject
		{
			get { return this.TypedPrincipal.GetUnderlyingObject(); }
		}

		public virtual Type UnderlyingObjectType
		{
			get { return this.TypedPrincipal.GetUnderlyingObjectType(); }
		}

		public virtual string UserPrincipalName
		{
			get { return this.TypedPrincipal.UserPrincipalName; }
			set { this.TypedPrincipal.UserPrincipalName = value; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "This is a wrapper.")]
		public virtual void Dispose()
		{
			if(this.DisposeContextOnDispose)
				this.TypedPrincipal.Context.Dispose();

			this.TypedPrincipal.Dispose();
		}

		public override bool Equals(object obj)
		{
			if(obj == null)
				return false;

			if(ReferenceEquals(this, obj))
				return true;

			var principalWrapper = obj as PrincipalWrapper<T>;

			if(principalWrapper != null)
				return this.TypedPrincipal.Equals(principalWrapper.TypedPrincipal);

			return false;
		}

		public override int GetHashCode()
		{
			return this.TypedPrincipal.GetHashCode();
		}

		public virtual bool IsMemberOf(IGroupPrincipal group)
		{
			return this.TypedPrincipal.IsMemberOf((GroupPrincipal) this.GetPrincipal(group));
		}

		public virtual bool IsMemberOf(IdentityType identityType, string identityValue)
		{
			return this.TypedPrincipal.IsMemberOf(this.TypedPrincipal.Context, identityType, identityValue);
		}

		#endregion
	}
}