using System;
using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.Extensions
{
	public static class PrincipalWrapperExtension
	{
		#region Fields

		private static volatile IPrincipalWrapperExtension _instance;
		private static readonly object _lockObject = new object();

		#endregion

		#region Properties

		public static IPrincipalWrapperExtension Instance
		{
			get
			{
				if(_instance == null)
				{
					lock(_lockObject)
					{
						if(_instance == null)
						{
							_instance = new DefaultPrincipalWrapperExtension();
						}
					}
				}

				return _instance;
			}
			set
			{
				if(Equals(_instance, value))
					return;

				lock(_lockObject)
				{
					_instance = value;
				}
			}
		}

		#endregion

		#region Methods

		public static IPrincipal Wrap(this object value, Principal principal)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			return Instance.Wrap(principal);
		}

		public static TAbstractPrincipal Wrap<TConcretePrincipal, TAbstractPrincipal>(this object value, TConcretePrincipal principal) where TConcretePrincipal : Principal where TAbstractPrincipal : IPrincipal
		{
			if(value == null)
				throw new ArgumentNullException("value");

			return Instance.Wrap<TConcretePrincipal, TAbstractPrincipal>(principal);
		}

		#endregion
	}
}