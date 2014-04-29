using System.DirectoryServices.AccountManagement;
using HansKindberg.DirectoryServices.AccountManagement.Connections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.AccountManagement.UnitTests.Connections
{
	[TestClass]
	public class PrincipalConnectionTest
	{
		#region Methods

		[TestMethod]
		public void ToString_Test()
		{
			const string expectedValue = "ContextType=Domain;Name=TestName;Container=TestContainer";

			var principalConnection = new PrincipalConnection
			{
				Container = "TestContainer",
				ContextType = ContextType.Domain,
				Name = "TestName"
			};

			Assert.AreEqual(expectedValue, principalConnection.ToString());
		}

		#endregion
	}
}