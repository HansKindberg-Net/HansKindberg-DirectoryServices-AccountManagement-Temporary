using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using HansKindberg.DirectoryServices.AccountManagement.Extensions;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class PrincipalCollectionWrapper : ICollection<IPrincipal>
	{
		#region Fields

		private readonly PrincipalCollection _principalCollection;

		#endregion

		#region Constructors

		public PrincipalCollectionWrapper(PrincipalCollection principalCollection)
		{
			if(principalCollection == null)
				throw new ArgumentNullException("principalCollection");

			this._principalCollection = principalCollection;
		}

		#endregion

		#region Properties

		public virtual int Count
		{
			get { return this.PrincipalCollection.Count; }
		}

		public virtual bool IsReadOnly
		{
			get { return this.PrincipalCollection.IsReadOnly; }
		}

		protected internal virtual PrincipalCollection PrincipalCollection
		{
			get { return this._principalCollection; }
		}

		#endregion

		#region Methods

		public virtual void Add(IPrincipal item)
		{
			this.PrincipalCollection.Add(this.GetPrincipal(item));
		}

		public virtual void Clear()
		{
			this.PrincipalCollection.Clear();
		}

		public virtual bool Contains(IPrincipal item)
		{
			return this.PrincipalCollection.Contains(this.GetPrincipal(item));
		}

		public virtual void CopyTo(IPrincipal[] array, int arrayIndex)
		{
			if(array == null)
				throw new ArgumentNullException("array");

			var principalArray = new Principal[array.Length];

			this.PrincipalCollection.CopyTo(principalArray, arrayIndex);

			for(int i = 0; i < array.Length; i++)
			{
				array[i] = this.Wrap(principalArray[i]);
			}
		}

		public static PrincipalCollectionWrapper FromPrincipalCollection(PrincipalCollection principalCollection)
		{
			return principalCollection;
		}

		public virtual IEnumerator<IPrincipal> GetEnumerator()
		{
			return new PrincipalCollectionEnumeratorWrapper(this.PrincipalCollection.GetEnumerator());
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public virtual bool Remove(IPrincipal item)
		{
			return this.PrincipalCollection.Remove(this.GetPrincipal(item));
		}

		#endregion

		#region Implicit operator

		public static implicit operator PrincipalCollectionWrapper(PrincipalCollection principalCollection)
		{
			return principalCollection != null ? new PrincipalCollectionWrapper(principalCollection) : null;
		}

		#endregion
	}
}