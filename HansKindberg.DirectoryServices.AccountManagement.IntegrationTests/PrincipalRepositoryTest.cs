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

		[TestMethod]
		public void Find_IfUsingAAuthenticablePrincipalQueryFilter_ShouldReturnAResultWithComputersAndUsers()
		{
			var principalConnection = new PrincipalConnection
			{
				ContextType = ContextType.Domain,
				Name = "LOCAL"
			};

			var principalRepository = new PrincipalRepository(principalConnection);

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

			Assert.AreEqual(2, principalTypes.Count);

			Assert.IsTrue(principalTypes.Contains(typeof(ComputerPrincipalWrapper)));
			Assert.IsTrue(principalTypes.Contains(typeof(UserPrincipalWrapper)));
		}

		[TestMethod]
		public void Find_IfUsingAPrincipalQueryFilter_ShouldReturnAResultWithComputersAndGroupsAndUsers()
		{
			var principalConnection = new PrincipalConnection
			{
				ContextType = ContextType.Domain,
				Name = "LOCAL"
			};

			var principalRepository = new PrincipalRepository(principalConnection);

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

			Assert.AreEqual(3, principalTypes.Count);

			Assert.IsTrue(principalTypes.Contains(typeof(ComputerPrincipalWrapper)));
			Assert.IsTrue(principalTypes.Contains(typeof(GroupPrincipalWrapper)));
			Assert.IsTrue(principalTypes.Contains(typeof(UserPrincipalWrapper)));
		}

		#endregion
	}
}