using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.DirectoryServices.AccountManagement.Collections.Generic
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class EnumerableWrapper<TIn, TOut> : IEnumerable<TOut>
	{
		#region Fields

		private readonly Func<TIn, TOut> _elementWrapper;
		private readonly IEnumerable<TIn> _enumerable;

		#endregion

		#region Constructors

		public EnumerableWrapper(IEnumerable<TIn> enumerable, Func<TIn, TOut> elementWrapper)
		{
			if(enumerable == null)
				throw new ArgumentNullException("enumerable");

			if(elementWrapper == null)
				throw new ArgumentNullException("elementWrapper");

			this._elementWrapper = elementWrapper;
			this._enumerable = enumerable;
		}

		#endregion

		#region Properties

		public virtual Func<TIn, TOut> ElementWrapper
		{
			get { return this._elementWrapper; }
		}

		public virtual IEnumerable<TIn> Enumerable
		{
			get { return this._enumerable; }
		}

		#endregion

		#region Methods

		public virtual IEnumerator<TOut> GetEnumerator()
		{
			return new EnumeratorWrapper<TIn, TOut>(this.Enumerable.GetEnumerator(), this.ElementWrapper);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion
	}
}