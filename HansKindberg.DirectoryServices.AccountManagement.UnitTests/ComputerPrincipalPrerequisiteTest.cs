using System.DirectoryServices.AccountManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.AccountManagement.UnitTests
{
	[TestClass]
	public class ComputerPrincipalPrerequisiteTest
	{
		#region Methods

		[TestMethod]
		public void Context_ShouldReturnAReferenceToThePrincipalContextParameterPassedInTheConstructor()
		{
			using(var principalContext = new PrincipalContext(ContextType.Machine))
			{
				using(var computerPrincipal = new ComputerPrincipal(principalContext))
				{
					Assert.IsTrue(ReferenceEquals(principalContext, computerPrincipal.Context));
				}
			}
		}

		#endregion
	}
}