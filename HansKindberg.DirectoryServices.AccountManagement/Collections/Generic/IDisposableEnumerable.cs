using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices.AccountManagement.Collections.Generic
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public interface IDisposableEnumerable<T> : IEnumerable<T>, IDisposable {}
}