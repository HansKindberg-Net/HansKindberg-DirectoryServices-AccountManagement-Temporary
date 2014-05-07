using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices.AccountManagement.Collections.Generic
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class EmptyDisposableEnumerable<T> : IDisposableEnumerable<T>
	{
		#region Fields

		private readonly IEnumerable<T> _enumerable = new T[0];

		#endregion

		#region Properties

		public virtual IEnumerable<T> Enumerable
		{
			get { return this._enumerable; }
		}

		#endregion

		#region Methods

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing) {}

		public virtual IEnumerator<T> GetEnumerator()
		{
			return this.Enumerable.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion
	}
}