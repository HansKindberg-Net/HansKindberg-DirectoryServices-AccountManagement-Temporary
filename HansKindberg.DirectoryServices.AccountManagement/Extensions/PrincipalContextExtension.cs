using System;
using System.DirectoryServices.AccountManagement;

namespace HansKindberg.DirectoryServices.AccountManagement.Extensions
{
	public static class PrincipalContextExtension
	{
		#region Fields

		private static volatile IPrincipalContextExtension _instance;
		private static readonly object _lockObject = new object();

		#endregion

		#region Properties

		public static IPrincipalContextExtension Instance
		{
			get
			{
				if(_instance == null)
				{
					lock(_lockObject)
					{
						if(_instance == null)
						{
							_instance = new DefaultPrincipalContextExtension();
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

		public static PrincipalContext GetPrincipalContext(this object value, IPrincipalContext principalContext)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			return Instance.GetPrincipalContext(principalContext);
		}

		public static PrincipalContext GetPrincipalContext(this object value, IPrincipalContext principalContext, bool throwExceptionIfUnsuccessful)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			return Instance.GetPrincipalContext(principalContext, throwExceptionIfUnsuccessful);
		}

		#endregion
	}
}