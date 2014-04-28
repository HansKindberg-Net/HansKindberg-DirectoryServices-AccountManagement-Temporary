using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using HansKindberg.DirectoryServices.AccountManagement.Extensions;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public abstract class PrincipalWrapper<T> : IPrincipal, IPrincipalInternal<T> where T : Principal
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