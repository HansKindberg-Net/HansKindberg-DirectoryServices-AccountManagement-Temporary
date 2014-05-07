using System;
using System.Collections;
using System.Collections.Generic;

namespace HansKindberg.DirectoryServices.AccountManagement.Collections.Generic
{
	public class EnumeratorWrapper<TIn, TOut> : IEnumerator<TOut>
	{
		#region Fields

		private readonly Func<TIn, TOut> _elementWrapper;
		private readonly IEnumerator<TIn> _enumerator;

		#endregion

		#region Constructors

		public EnumeratorWrapper(IEnumerator<TIn> enumerator, Func<TIn, TOut> elementWrapper)
		{
			if(enumerator == null)
				throw new ArgumentNullException("enumerator");

			if(elementWrapper == null)
				throw new ArgumentNullException("elementWrapper");

			this._elementWrapper = elementWrapper;
			this._enumerator = enumerator;
		}

		#endregion

		#region Properties

		public virtual TOut Current
		{
			get { return this.ElementWrapper(this.Enumerator.Current); }
		}

		object IEnumerator.Current
		{
			get { return this.Current; }
		}

		public virtual Func<TIn, TOut> ElementWrapper
		{
			get { return this._elementWrapper; }
		}

		public virtual IEnumerator<TIn> Enumerator
		{
			get { return this._enumerator; }
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
				this.Enumerator.Dispose();
		}

		public virtual bool MoveNext()
		{
			return this.Enumerator.MoveNext();
		}

		public virtual void Reset()
		{
			this.Enumerator.Reset();
		}

		#endregion
	}
}