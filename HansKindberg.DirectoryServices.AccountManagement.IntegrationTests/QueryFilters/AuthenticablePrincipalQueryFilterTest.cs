using System.DirectoryServices.AccountManagement;
using HansKindberg.DirectoryServices.AccountManagement.QueryFilters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.AccountManagement.IntegrationTests.QueryFilters
{
	[TestClass]
	public class AuthenticablePrincipalQueryFilterTest
	{
		#region Methods

		[TestMethod]
		public void CreateConcreteQueryFilter_ShouldOnlyTransferSetQueryFiltersToTheConcreteQueryFilter()
		{
			const string descriptionFilter = "Test";
			const bool enabledFilter = true;
			const string homeDirectoryFilter = "Test";

			using(var authenticablePrincipalQueryFilter = new AuthenticablePrincipalQueryFilter())
			{
				authenticablePrincipalQueryFilter.Description = descriptionFilter;
				authenticablePrincipalQueryFilter.Enabled = enabledFilter;
				authenticablePrincipalQueryFilter.HomeDirectory = homeDirectoryFilter;

				using(var concretePrincipalContext = CreateDefaultDomainPrincipalContext())
				{
					using(var principalContext = (PrincipalContextWrapper) concretePrincipalContext)
					{
						var concreteQueryFilter = (IAuthenticablePrincipal) authenticablePrincipalQueryFilter.CreateConcreteQueryFilter(principalContext);

						Assert.IsNull(concreteQueryFilter.AccountExpirationDate);
						Assert.IsNull(concreteQueryFilter.AccountLockoutTime);
						Assert.AreEqual(descriptionFilter, concreteQueryFilter.Description);
						Assert.AreEqual(enabledFilter, concreteQueryFilter.Enabled);
						Assert.AreEqual(homeDirectoryFilter, concreteQueryFilter.HomeDirectory);
						Assert.IsNull(concreteQueryFilter.HomeDrive);
						Assert.IsNull(concreteQueryFilter.Name);
					}
				}
			}
		}

		private static PrincipalContext CreateDefaultDomainPrincipalContext()
		{
			return new PrincipalContext(ContextType.Domain, "LOCAL");
		}

		#endregion
	}
}