using System.DirectoryServices.AccountManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.AccountManagement.UnitTests
{
	[TestClass]
	public class UserPrincipalPrerequisiteTest
	{
		#region Methods

		[TestMethod]
		public void Context_ShouldReturnAReferenceToThePrincipalContextParameterPassedInTheConstructor()
		{
			using(var principalContext = new PrincipalContext(ContextType.Machine))
			{
				using(var userPrincipal = new UserPrincipal(principalContext))
				{
					Assert.IsTrue(ReferenceEquals(principalContext, userPrincipal.Context));
				}
			}
		}

		#endregion
	}
}