using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public interface IPrincipalRepository<T> where T : IPrincipal
	{
		#region Properties

		int PageSize { get; set; }
		int? SizeLimit { get; set; }

		#endregion

		#region Methods

		TEditablePrincipal Create<TEditablePrincipal>() where TEditablePrincipal : IEditablePrincipal;
		TEditablePrincipal Create<TEditablePrincipal>(string container) where TEditablePrincipal : IEditablePrincipal;
		TEditablePrincipal Create<TEditablePrincipal>(IPrincipalContext principalContext) where TEditablePrincipal : IEditablePrincipal;
		void Delete(IEditablePrincipal principal);
		IDisposableEnumerable<T> Find(T queryFilter);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		T Get(string identity);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		T Get(string identity, IdentityType identityType);

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		TPrincipal Get<TPrincipal>(string identity) where TPrincipal : IPrincipal;

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		TPrincipal Get<TPrincipal>(string identity, IdentityType identityType) where TPrincipal : IPrincipal;

		void Save(IEditablePrincipal principal);
		void Save(IEditablePrincipal principal, IPrincipalContext principalContext);

		#endregion
	}
}