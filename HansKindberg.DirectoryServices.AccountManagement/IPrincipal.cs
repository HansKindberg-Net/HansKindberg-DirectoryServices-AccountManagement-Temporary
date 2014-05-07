using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IPrincipal : IDisposable
	{
		#region Properties

		IPrincipalContext Context { get; }
		ContextType ContextType { get; }
		string Description { get; set; }
		string DisplayName { get; set; }
		string DistinguishedName { get; }
		IDisposableEnumerable<IGroupPrincipal> Groups { get; }
		Guid? Guid { get; }
		string Name { get; set; }
		string SamAccountName { get; set; }
		SecurityIdentifier Sid { get; }
		string StructuralObjectClass { get; }
		object UnderlyingObject { get; }
		Type UnderlyingObjectType { get; }
		string UserPrincipalName { get; set; }

		#endregion

		#region Methods

		bool IsMemberOf(IGroupPrincipal group);
		bool IsMemberOf(IdentityType identityType, string identityValue);

		#endregion
	}
}