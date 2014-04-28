using System.DirectoryServices.AccountManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.AccountManagement.UnitTests
{
	[TestClass]
	public class GroupPrincipalPrerequisiteTest
	{
		#region Methods

		[TestMethod]
		public void Context_ShouldReturnAReferenceToThePrincipalContextParameterPassedInTheConstructor()
		{
			using(var principalContext = new PrincipalContext(ContextType.Machine))
			{
				using(var groupPrincipal = new GroupPrincipal(principalContext))
				{
					Assert.IsTrue(ReferenceEquals(principalContext, groupPrincipal.Context));
				}
			}
		}

		#endregion
	}
}