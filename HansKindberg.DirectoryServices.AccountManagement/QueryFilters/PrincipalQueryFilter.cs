using System;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;
using HansKindberg.DirectoryServices.AccountManagement.Extensions;

namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public class PrincipalQueryFilter : PrincipalQueryFilter<IPrincipal> {}

	public abstract class PrincipalQueryFilter<T> : IPrincipal, IPrincipalQueryFilterInternal where T : IPrincipal
	{
		#region Fields

		private IQueryFilterValue<string> _descriptionValue;
		private IQueryFilterValue<string> _displayNameValue;
		private IQueryFilterValue<string> _nameValue;
		private IQueryFilterValue<string> _samAccountNameValue;
		private IQueryFilterValue<string> _userPrincipalNameValue;

		#endregion

		#region Properties

		public virtual IPrincipalContext Context
		{
			get { return null; }
		}

		public virtual ContextType ContextType
		{
			get { return ContextType.Machine; }
		}

		public virtual string Description
		{
			get { return this.DescriptionValue.Value; }
			set { this.DescriptionValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<string> DescriptionValue
		{
			get { return this._descriptionValue ?? (this._descriptionValue = new QueryFilterValue<string>()); }
		}

		public virtual string DisplayName
		{
			get { return this.DisplayNameValue.Value; }
			set { this.DisplayNameValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<string> DisplayNameValue
		{
			get { return this._displayNameValue ?? (this._displayNameValue = new QueryFilterValue<string>()); }
		}

		public virtual string DistinguishedName
		{
			get { return null; }
		}

		public virtual IDisposableEnumerable<IGroupPrincipal> Groups
		{
			get { return new EmptyDisposableEnumerable<IGroupPrincipal>(); }
		}

		public virtual Guid? Guid
		{
			get { return null; }
		}

		public virtual string Name
		{
			get { return this.NameValue.Value; }
			set { this.NameValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<string> NameValue
		{
			get { return this._nameValue ?? (this._nameValue = new QueryFilterValue<string>()); }
		}

		public virtual string SamAccountName
		{
			get { return this.SamAccountNameValue.Value; }
			set { this.SamAccountNameValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<string> SamAccountNameValue
		{
			get { return this._samAccountNameValue ?? (this._samAccountNameValue = new QueryFilterValue<string>()); }
		}

		public virtual SecurityIdentifier Sid
		{
			get { return null; }
		}

		public virtual string StructuralObjectClass
		{
			get { return null; }
		}

		public virtual object UnderlyingObject
		{
			get { return null; }
		}

		public virtual Type UnderlyingObjectType
		{
			get { return null; }
		}

		public virtual string UserPrincipalName
		{
			get { return this.UserPrincipalNameValue.Value; }
			set { this.UserPrincipalNameValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<string> UserPrincipalNameValue
		{
			get { return this._userPrincipalNameValue ?? (this._userPrincipalNameValue = new QueryFilterValue<string>()); }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller. ")]
		public virtual IPrincipal CreateConcreteQueryFilter(IPrincipalContext principalContext)
		{
			T concreteQueryFilter = (T) (object) (PrincipalWrapper) new GeneralPrincipal(this.GetPrincipalContext(principalContext));

			this.TransferQueryFilter(concreteQueryFilter);

			return concreteQueryFilter;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing) {}

		public virtual bool IsMemberOf(IGroupPrincipal group)
		{
			return false;
		}

		public virtual bool IsMemberOf(IdentityType identityType, string identityValue)
		{
			return false;
		}

		protected internal virtual void TransferQueryFilter(T queryFilter)
		{
			if(Equals(queryFilter, null))
				throw new ArgumentNullException("queryFilter");

			if(this.DescriptionValue.IsSet)
				queryFilter.Description = this.Description;

			if(this.DisplayNameValue.IsSet)
				queryFilter.DisplayName = this.DisplayName;

			if(this.NameValue.IsSet)
				queryFilter.Name = this.Name;

			if(this.SamAccountNameValue.IsSet)
				queryFilter.SamAccountName = this.SamAccountName;

			if(this.UserPrincipalNameValue.IsSet)
				queryFilter.UserPrincipalName = this.UserPrincipalName;
		}

		#endregion
	}
}