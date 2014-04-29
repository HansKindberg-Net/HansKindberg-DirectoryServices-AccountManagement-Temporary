using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class PrincipalSearchResult<T> : IDisposableEnumerable<T> where T : IPrincipal
	{
		#region Fields

		private readonly bool _disposePrincipalContextOnDispose;
		private readonly IEnumerable<T> _items;
		private readonly IPrincipalContext _principalContext;

		#endregion

		#region Constructors

		public PrincipalSearchResult(IEnumerable<T> items, IPrincipalContext principalContext, bool disposePrincipalContextOnDispose)
		{
			if(items == null)
				throw new ArgumentNullException("items");

			if(principalContext == null)
				throw new ArgumentNullException("principalContext");

			this._disposePrincipalContextOnDispose = disposePrincipalContextOnDispose;
			this._items = items;
			this._principalContext = principalContext;
		}

		#endregion

		#region Properties

		protected internal virtual bool DisposePrincipalContextOnDispose
		{
			get { return this._disposePrincipalContextOnDispose; }
		}

		protected internal virtual IEnumerable<T> Items
		{
			get { return this._items; }
		}

		protected internal virtual IPrincipalContext PrincipalContext
		{
			get { return this._principalContext; }
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
			if(!disposing)
				return;

			if(this.DisposePrincipalContextOnDispose)
				this.PrincipalContext.Dispose();

			foreach(var item in this.Items)
			{
				item.Dispose();
			}
		}

		public virtual IEnumerator<T> GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion
	}
}