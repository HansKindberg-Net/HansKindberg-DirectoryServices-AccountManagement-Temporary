﻿using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IUserPrincipal : IAuthenticablePrincipal, IEditablePrincipal
	{
		#region Properties

		IDisposableEnumerable<IGroupPrincipal> AuthorizationGroups { get; }
		string EmailAddress { get; set; }
		string EmployeeId { get; set; }
		string GivenName { get; set; }
		string MiddleName { get; set; }
		string Surname { get; set; }
		string VoiceTelephoneNumber { get; set; }

		#endregion
	}
}