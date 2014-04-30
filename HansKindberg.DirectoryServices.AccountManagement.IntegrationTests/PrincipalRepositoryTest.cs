using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using HansKindberg.DirectoryServices.AccountManagement.Connections;
using HansKindberg.DirectoryServices.AccountManagement.QueryFilters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.AccountManagement.IntegrationTests
{
	[TestClass]
	public class PrincipalRepositoryTest
	{
		#region Methods

		private static IPrincipalConnection CreateDefaultDomainPrincipalConnection()
		{
			return new PrincipalConnection
			{
				ContextType = ContextType.Domain,
				Name = "LOCAL"
			};
		}

		[TestMethod]
		public void Create_ShouldHandleUserPrincipal()
		{
			var principalRepository = new PrincipalRepository(CreateDefaultDomainPrincipalConnection());

			using(var userPrincipal = principalRepository.Create<IUserPrincipal>())
			{
				Assert.IsNotNull(userPrincipal);
			}
		}

		[TestMethod]
		public void Find_IfUsingAAuthenticablePrincipalQueryFilter_ShouldReturnAResultWithComputersAndUsers()
		{
			var principalRepository = new PrincipalRepository(CreateDefaultDomainPrincipalConnection());

			var structuralObjectClasses = new List<string>();
			var principalTypes = new List<Type>();

			using(var queryFilter = new AuthenticablePrincipalQueryFilter())
			{
				using(var searchResult = principalRepository.Find(queryFilter))
				{
					foreach(var item in searchResult)
					{
						if(!structuralObjectClasses.Contains(item.StructuralObjectClass, StringComparer.OrdinalIgnoreCase))
							structuralObjectClasses.Add(item.StructuralObjectClass);

						if(!principalTypes.Contains(item.GetType()))
							principalTypes.Add(item.GetType());
					}
				}
			}

			Assert.AreEqual(2, structuralObjectClasses.Count);

			Assert.IsTrue(structuralObjectClasses.Contains("computer"));
			Assert.IsTrue(structuralObjectClasses.Contains("user"));

			Assert.AreEqual(1, principalTypes.Count);

			Assert.IsTrue(principalTypes.Contains(typeof(AuthenticablePrincipalWrapper)));
		}

		[TestMethod]
		public void Find_IfUsingAPrincipalQueryFilter_ShouldReturnAResultWithComputersAndGroupsAndUsers()
		{
			var principalRepository = new PrincipalRepository(CreateDefaultDomainPrincipalConnection());

			var structuralObjectClasses = new List<string>();
			var principalTypes = new List<Type>();

			using(var queryFilter = new PrincipalQueryFilter())
			{
				using(var searchResult = principalRepository.Find(queryFilter))
				{
					foreach(var item in searchResult)
					{
						if(!structuralObjectClasses.Contains(item.StructuralObjectClass, StringComparer.OrdinalIgnoreCase))
							structuralObjectClasses.Add(item.StructuralObjectClass);

						if(!principalTypes.Contains(item.GetType()))
							principalTypes.Add(item.GetType());
					}
				}
			}

			Assert.AreEqual(3, structuralObjectClasses.Count);

			Assert.IsTrue(structuralObjectClasses.Contains("computer"));
			Assert.IsTrue(structuralObjectClasses.Contains("group"));
			Assert.IsTrue(structuralObjectClasses.Contains("user"));

			Assert.AreEqual(1, principalTypes.Count);

			Assert.IsTrue(principalTypes.Contains(typeof(PrincipalWrapper)));
		}

		[TestMethod]
		public void Save_ShouldHandleUserPrincipal()
		{
			const string userName = "User1000";

			var principalRepository = new PrincipalRepository(CreateDefaultDomainPrincipalConnection());

			using(var userPrincipalQueryFilter = new UserPrincipalQueryFilter())
			{
				userPrincipalQueryFilter.SamAccountName = userName;

				using(var foundUserPrincipals = principalRepository.Find(userPrincipalQueryFilter))
				{
					if(foundUserPrincipals.Any())
					{
						if(foundUserPrincipals.Count() > 1)
							throw new InvalidOperationException("There should not be duplicates of a user.");

						var userPrincipal = (IUserPrincipal) foundUserPrincipals.ElementAt(0);

						principalRepository.Delete(userPrincipal);
					}
				}
			}

			using(var userPrincipal = principalRepository.Create<IUserPrincipal>())
			{
				userPrincipal.Enabled = true;
				userPrincipal.PasswordNeverExpires = true;
				userPrincipal.SamAccountName = userName;
				userPrincipal.SetPassword("P@ssword");

				principalRepository.Save(userPrincipal);
			}
		}

		#endregion
	}
}