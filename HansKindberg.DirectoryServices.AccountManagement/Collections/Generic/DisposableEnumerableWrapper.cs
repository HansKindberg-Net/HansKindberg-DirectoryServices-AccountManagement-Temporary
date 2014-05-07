using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace HansKindberg.DirectoryServices.AccountManagement.Collections.Generic
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class DisposableEnumerableWrapper<TIn, TOut> : EnumerableWrapper<TIn, TOut>, IDisposableEnumerable<TOut>
	{
		#region Constructors

		public DisposableEnumerableWrapper(IEnumerable<TIn> disposableEnumerable, Func<TIn, TOut> elementWrapper) : base(disposableEnumerable, elementWrapper)
		{
			if(!(disposableEnumerable is IDisposable))
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The disposable enumerable must implement \"{0}\".", typeof(IDisposable)), "disposableEnumerable");
		}

		#endregion

		#region Properties

		public virtual IDisposable DisposableEnumerable
		{
			get { return (IDisposable) this.Enumerable; }
		}

		#endregion

		#region Methods

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if(disposing)
			{
				var disposableEnumerable = this.DisposableEnumerable;

				if(disposableEnumerable != null)
					disposableEnumerable.Dispose();
			}
		}

		#endregion
	}
}