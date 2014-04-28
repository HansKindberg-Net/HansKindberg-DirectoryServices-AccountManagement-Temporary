using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.AccountManagement.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.DirectoryServices.AccountManagement.ShimTests
{
	[TestClass]
	public class ComputerPrincipalWrapperTest
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

				var computerPrincipalWrapper = new ComputerPrincipalWrapper(new ComputerPrincipal(shimPrincipalContext))
				{
					DisposeContextOnDispose = false
				};

				Assert.IsFalse(disposeIsCalled);
				computerPrincipalWrapper.Dispose();
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

				var computerPrincipalWrapper = new ComputerPrincipalWrapper(new ComputerPrincipal(shimPrincipalContext))
				{
					DisposeContextOnDispose = true
				};

				Assert.IsFalse(disposeIsCalled);
				computerPrincipalWrapper.Dispose();
				Assert.IsTrue(disposeIsCalled);
			}
		}

		[TestMethod]
		public void Dispose_ShouldCallDisposeOfTheWrappedComputerPrincipal()
		{
			using(ShimsContext.Create())
			{
				bool disposeIsCalled = false;

				var shimComputerPrincipal = new ShimComputerPrincipal();

				// ReSharper disable ObjectCreationAsStatement
				new ShimPrincipal(shimComputerPrincipal)
				{
					Dispose = delegate { disposeIsCalled = true; }
				};
				// ReSharper restore ObjectCreationAsStatement

				Assert.IsFalse(disposeIsCalled);
				new ComputerPrincipalWrapper(shimComputerPrincipal).Dispose();
				Assert.IsTrue(disposeIsCalled);
			}
		}

		#endregion
	}
}