using System;
using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.Extensions
{
	public static class PrincipalExtension
	{
		#region Fields

		private static volatile IPrincipalExtension _instance;
		private static readonly object _lockObject = new object();

		#endregion

		#region Properties

		public static IPrincipalExtension Instance
		{
			get
			{
				if(_instance == null)
				{
					lock(_lockObject)
					{
						if(_instance == null)
						{
							_instance = new DefaultPrincipalExtension();
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

		public static Principal GetPrincipal(this object value, IPrincipal principal)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			return Instance.GetPrincipal(principal);
		}

		public static Principal GetPrincipal(this object value, IPrincipal principal, bool throwExceptionIfUnsuccessful)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			return Instance.GetPrincipal(principal, throwExceptionIfUnsuccessful);
		}

		#endregion
	}
}