using System.DirectoryServices.AccountManagement;
using HansKindberg.DirectoryServices.AccountManagement.Connections;
using HansKindberg.DirectoryServices.AccountManagement.QueryFilters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.AccountManagement.IntegrationTests
{
	[TestClass]
	public class PrincipalRepositoryTest
	{
		#region Methods

		public void GeneralTest()
		{
			var principalConnection = new PrincipalConnection {ContextType = ContextType.ApplicationDirectory};
			var principalRepository = new PrincipalRepository(principalConnection);

			using(var searchResult = principalRepository.Find(new PrincipalQueryFilter()))
			{
				foreach(var item in searchResult)
				{
					var principal = item;
				}
			}
		}

		#endregion
	}
}