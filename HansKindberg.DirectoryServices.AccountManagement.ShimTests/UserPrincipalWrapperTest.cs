using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.AccountManagement.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.AccountManagement.ShimTests
{
	[TestClass]
	public class UserPrincipalWrapperTest
	{
		#region Methods

		[TestMethod]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public void Dispose_IfDisposeContextOnDisposeIsFalse_ShouldNotDisposeTheContext()
		{
			using(ShimsContext.Create())
			{
				bool disposeIsCalled = false;

				var shimPrincipalContext = new ShimPrincipalContext()
				{
					Dispose = delegate { disposeIsCalled = true; }
				};

				var userPrincipalWrapper = new UserPrincipalWrapper(new UserPrincipal(shimPrincipalContext))
				{
					DisposeContextOnDispose = false
				};

				Assert.IsFalse(disposeIsCalled);
				userPrincipalWrapper.Dispose();
				Assert.IsFalse(disposeIsCalled);
			}
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public void Dispose_IfDisposeContextOnDisposeIsTrue_ShouldDisposeTheContext()
		{
			using(ShimsContext.Create())
			{
				bool disposeIsCalled = false;

				var shimPrincipalContext = new ShimPrincipalContext()
				{
					Dispose = delegate { disposeIsCalled = true; }
				};

				var userPrincipalWrapper = new UserPrincipalWrapper(new UserPrincipal(shimPrincipalContext))
				{
					DisposeContextOnDispose = true
				};

				Assert.IsFalse(disposeIsCalled);
				userPrincipalWrapper.Dispose();
				Assert.IsTrue(disposeIsCalled);
			}
		}

		[TestMethod]
		public void Dispose_ShouldCallDisposeOfTheWrappedUserPrincipal()
		{
			using(ShimsContext.Create())
			{
				bool disposeIsCalled = false;

				var shimUserPrincipal = new ShimUserPrincipal();

				// ReSharper disable ObjectCreationAsStatement
				new ShimPrincipal(shimUserPrincipal)
				{
					Dispose = delegate { disposeIsCalled = true; }
				};
				// ReSharper restore ObjectCreationAsStatement

				Assert.IsFalse(disposeIsCalled);
				new UserPrincipalWrapper(shimUserPrincipal).Dispose();
				Assert.IsTrue(disposeIsCalled);
			}
		}

		#endregion
	}
}