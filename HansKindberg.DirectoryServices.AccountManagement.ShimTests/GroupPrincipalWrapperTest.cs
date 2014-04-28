using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.AccountManagement.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.AccountManagement.ShimTests
{
	[TestClass]
	public class GroupPrincipalWrapperTest
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

				var groupPrincipalWrapper = new GroupPrincipalWrapper(new GroupPrincipal(shimPrincipalContext))
				{
					DisposeContextOnDispose = false
				};

				Assert.IsFalse(disposeIsCalled);
				groupPrincipalWrapper.Dispose();
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

				var groupPrincipalWrapper = new GroupPrincipalWrapper(new GroupPrincipal(shimPrincipalContext))
				{
					DisposeContextOnDispose = true
				};

				Assert.IsFalse(disposeIsCalled);
				groupPrincipalWrapper.Dispose();
				Assert.IsTrue(disposeIsCalled);
			}
		}

		[TestMethod]
		public void Dispose_ShouldCallDisposeOfTheWrappedGroupPrincipal()
		{
			using(ShimsContext.Create())
			{
				bool disposeIsCalled = false;

				var shimGroupPrincipal = new ShimGroupPrincipal()
				{
					Dispose = delegate { disposeIsCalled = true; }
				};

				Assert.IsFalse(disposeIsCalled);
				new GroupPrincipalWrapper(shimGroupPrincipal).Dispose();
				Assert.IsTrue(disposeIsCalled);
			}
		}

		#endregion
	}
}