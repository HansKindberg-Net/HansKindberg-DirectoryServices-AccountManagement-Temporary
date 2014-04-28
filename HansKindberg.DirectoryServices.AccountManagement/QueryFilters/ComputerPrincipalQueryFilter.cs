using System.Collections.Generic;

namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public class ComputerPrincipalQueryFilter : AuthenticablePrincipalQueryFilter, IComputerPrincipalQueryFilter
	{
		#region Fields

		private readonly IList<string> _servicePrincipalNames = new List<string>();

		#endregion

		#region Properties

		public virtual IList<string> ServicePrincipalNames
		{
			get { return this._servicePrincipalNames; }
		}

		#endregion
	}
}