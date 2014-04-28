using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using HansKindberg.DirectoryServices.AccountManagement.Extensions;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public class PrincipalCollectionEnumeratorWrapper : IEnumerator<IPrincipal>
	{
		#region Fields

		private readonly IEnumerator<Principal> _principalCollectionEnumerator;

		#endregion

		#region Constructors

		public PrincipalCollectionEnumeratorWrapper(IEnumerator<Principal> principalCollectionEnumerator)
		{
			if(principalCollectionEnumerator == null)
				throw new ArgumentNullException("principalCollectionEnumerator");

			this._principalCollectionEnumerator = principalCollectionEnumerator;
		}

		#endregion

		#region Properties

		public virtual IPrincipal Current
		{
			get { return this.Wrap(this.PrincipalCollectionEnumerator.Current); }
		}

		object IEnumerator.Current
		{
			get { return this.Current; }
		}

		protected internal virtual IEnumerator<Principal> PrincipalCollectionEnumerator
		{
			get { return this._principalCollectionEnumerator; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "This is a wrapper.")]
		public virtual void Dispose()
		{
			this.PrincipalCollectionEnumerator.Dispose();
		}

		public virtual bool MoveNext()
		{
			return this.PrincipalCollectionEnumerator.MoveNext();
		}

		public virtual void Reset()
		{
			this.PrincipalCollectionEnumerator.Reset();
		}

		#endregion
	}
}