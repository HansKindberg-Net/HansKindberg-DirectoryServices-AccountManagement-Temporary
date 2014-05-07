using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;
using HansKindberg.DirectoryServices.AccountManagement.Extensions;

namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public class UserPrincipalQueryFilter : UserPrincipalQueryFilter<IUserPrincipal> {}

	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public abstract class UserPrincipalQueryFilter<T> : AuthenticablePrincipalQueryFilter<T>, IUserPrincipal where T : IUserPrincipal
	{
		#region Fields

		private IQueryFilterValue<string> _emailAddressValue;
		private IQueryFilterValue<string> _employeeIdValue;
		private IQueryFilterValue<string> _givenNameValue;
		private IQueryFilterValue<string> _middleNameValue;
		private IQueryFilterValue<string> _surnameValue;
		private IQueryFilterValue<string> _voiceTelephoneNumberValue;

		#endregion

		#region Properties

		public virtual IDisposableEnumerable<IGroupPrincipal> AuthorizationGroups
		{
			get { return new EmptyDisposableEnumerable<IGroupPrincipal>(); }
		}

		public virtual string EmailAddress
		{
			get { return this.EmailAddressValue.Value; }
			set { this.EmailAddressValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<string> EmailAddressValue
		{
			get { return this._emailAddressValue ?? (this._emailAddressValue = new QueryFilterValue<string>()); }
		}

		public virtual string EmployeeId
		{
			get { return this.EmployeeIdValue.Value; }
			set { this.EmployeeIdValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<string> EmployeeIdValue
		{
			get { return this._employeeIdValue ?? (this._employeeIdValue = new QueryFilterValue<string>()); }
		}

		public virtual string GivenName
		{
			get { return this.GivenNameValue.Value; }
			set { this.GivenNameValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<string> GivenNameValue
		{
			get { return this._givenNameValue ?? (this._givenNameValue = new QueryFilterValue<string>()); }
		}

		public virtual string MiddleName
		{
			get { return this.MiddleNameValue.Value; }
			set { this.MiddleNameValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<string> MiddleNameValue
		{
			get { return this._middleNameValue ?? (this._middleNameValue = new QueryFilterValue<string>()); }
		}

		public virtual string Surname
		{
			get { return this.SurnameValue.Value; }
			set { this.SurnameValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<string> SurnameValue
		{
			get { return this._surnameValue ?? (this._surnameValue = new QueryFilterValue<string>()); }
		}

		public virtual string VoiceTelephoneNumber
		{
			get { return this.VoiceTelephoneNumberValue.Value; }
			set { this.VoiceTelephoneNumberValue.Value = value; }
		}

		protected internal virtual IQueryFilterValue<string> VoiceTelephoneNumberValue
		{
			get { return this._voiceTelephoneNumberValue ?? (this._voiceTelephoneNumberValue = new QueryFilterValue<string>()); }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller. ")]
		public override IPrincipal CreateConcreteQueryFilter(IPrincipalContext principalContext)
		{
			T concreteQueryFilter = (T) (object) (UserPrincipalWrapper) new UserPrincipal(this.GetPrincipalContext(principalContext));

			this.TransferQueryFilter(concreteQueryFilter);

			return concreteQueryFilter;
		}

		protected internal override void TransferQueryFilter(T queryFilter)
		{
			base.TransferQueryFilter(queryFilter);

			if(this.EmailAddressValue.IsSet)
				queryFilter.EmailAddress = this.EmailAddress;

			if(this.EmployeeIdValue.IsSet)
				queryFilter.EmployeeId = this.EmployeeId;

			if(this.GivenNameValue.IsSet)
				queryFilter.GivenName = this.GivenName;

			if(this.MiddleNameValue.IsSet)
				queryFilter.MiddleName = this.MiddleName;

			if(this.SurnameValue.IsSet)
				queryFilter.Surname = this.Surname;

			if(this.VoiceTelephoneNumberValue.IsSet)
				queryFilter.VoiceTelephoneNumber = this.VoiceTelephoneNumber;
		}

		#endregion
	}
}