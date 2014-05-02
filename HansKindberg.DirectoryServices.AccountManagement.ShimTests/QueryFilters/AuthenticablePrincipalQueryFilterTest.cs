using System;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.AccountManagement.Fakes;
using HansKindberg.DirectoryServices.AccountManagement.QueryFilters;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.AccountManagement.ShimTests.QueryFilters
{
	[TestClass]
	public class AuthenticablePrincipalQueryFilterTest
	{
		#region Methods

		[TestMethod]
		public void CreateConcreteQueryFilter_ShouldOnlyTransferSetQueryFiltersToTheConcreteQueryFilter()
		{
			var accountExpirationDateFilter = DateTime.Now;
			const MatchType accountExpirationDateFilterMatchType = MatchType.LessThan;
			const string descriptionFilter = "Test";
			const bool enabledFilter = true;
			const string homeDirectoryFilter = "Test";

			using(ShimsContext.Create())
			{
				bool advancedSearchFilterAccountExpirationDateIsCalled = false;
				bool advancedSearchFilterAccountLockoutTimeIsCalled = false;
				bool descriptionSetIsCalled = false;
				bool enabledSetIsCalled = false;
				bool homeDirectorySetIsCalled = false;
				bool homeDriveSetIsCalled = false;
				bool nameSetIsCalled = false;

				ShimAdvancedFilters.AllInstances.AccountExpirationDateDateTimeMatchType = delegate(AdvancedFilters advancedFilters, DateTime dateTime, MatchType matchType)
				{
					if(dateTime == accountExpirationDateFilter && matchType == accountExpirationDateFilterMatchType)
						advancedSearchFilterAccountExpirationDateIsCalled = true;
				};

				ShimAdvancedFilters.AllInstances.AccountLockoutTimeDateTimeMatchType = delegate { advancedSearchFilterAccountLockoutTimeIsCalled = true; };

				ShimPrincipal.AllInstances.DescriptionSetString = delegate(Principal principal, string description)
				{
					if(description == descriptionFilter)
						descriptionSetIsCalled = true;
				};

				ShimPrincipal.AllInstances.NameSetString = delegate { nameSetIsCalled = true; };

				ShimAuthenticablePrincipal.AllInstances.EnabledSetNullableOfBoolean = delegate(AuthenticablePrincipal authenticablePrincipal, bool? enabled)
				{
					if(enabled == true)
						enabledSetIsCalled = true;
				};

				ShimAuthenticablePrincipal.AllInstances.HomeDirectorySetString = delegate(AuthenticablePrincipal authenticablePrincipal, string homeDirectory)
				{
					if(homeDirectory == homeDirectoryFilter)
						homeDirectorySetIsCalled = true;
				};

				ShimAuthenticablePrincipal.AllInstances.HomeDriveSetString = delegate { homeDriveSetIsCalled = true; };

				using(var authenticablePrincipalQueryFilter = new AuthenticablePrincipalQueryFilter())
				{
					authenticablePrincipalQueryFilter.AdvancedSearchFilter.AccountExpirationDate(accountExpirationDateFilter, accountExpirationDateFilterMatchType);
					authenticablePrincipalQueryFilter.Description = descriptionFilter;
					// ReSharper disable ConditionIsAlwaysTrueOrFalse
					authenticablePrincipalQueryFilter.Enabled = enabledFilter;
					// ReSharper restore ConditionIsAlwaysTrueOrFalse
					authenticablePrincipalQueryFilter.HomeDirectory = homeDirectoryFilter;

					using(var concretePrincipalContext = new PrincipalContext(ContextType.Machine))
					{
						using(var principalContext = (PrincipalContextWrapper) concretePrincipalContext)
						{
							Assert.IsFalse(advancedSearchFilterAccountExpirationDateIsCalled);
							Assert.IsFalse(advancedSearchFilterAccountLockoutTimeIsCalled);
							Assert.IsFalse(descriptionSetIsCalled);
							Assert.IsFalse(enabledSetIsCalled);
							Assert.IsFalse(homeDirectorySetIsCalled);
							Assert.IsFalse(homeDriveSetIsCalled);
							Assert.IsFalse(nameSetIsCalled);

							var concreteQueryFilter = (IAuthenticablePrincipal) authenticablePrincipalQueryFilter.CreateConcreteQueryFilter(principalContext);

							Assert.IsTrue(advancedSearchFilterAccountExpirationDateIsCalled);
							Assert.IsFalse(advancedSearchFilterAccountLockoutTimeIsCalled);
							Assert.IsTrue(descriptionSetIsCalled);
							Assert.IsTrue(enabledSetIsCalled);
							Assert.IsTrue(homeDirectorySetIsCalled);
							Assert.IsFalse(homeDriveSetIsCalled);
							Assert.IsFalse(nameSetIsCalled);

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
		}

		#endregion
	}
}