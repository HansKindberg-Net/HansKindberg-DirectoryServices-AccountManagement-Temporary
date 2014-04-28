using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using HansKindberg.DirectoryServices.AccountManagement.Collections.Generic;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class PrincipalSearchResult<T> : IDisposableEnumerable<T> where T : IPrincipal
	{
		#region Fields

		private readonly IEnumerable<T> _items;
		private readonly PrincipalContext _principalContext;

		#endregion

		#region Constructors

		public PrincipalSearchResult(PrincipalContext principalContext, IEnumerable<T> items)
		{
			if(principalContext == null)
				throw new ArgumentNullException("principalContext");

			if(items == null)
				throw new ArgumentNullException("items");

			this._items = items;
			this._principalContext = principalContext;
		}

		#endregion

		#region Properties

		protected internal virtual IEnumerable<T> Items
		{
			get { return this._items; }
		}

		protected internal virtual PrincipalContext PrincipalContext
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