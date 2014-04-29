using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HansKindberg.DirectoryServices.AccountManagement.QueryFilters
{
	public class ComputerPrincipalQueryFilter : ComputerPrincipalQueryFilter<IComputerPrincipal> {}

	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public abstract class ComputerPrincipalQueryFilter<T> : AuthenticablePrincipalQueryFilter<T>, IComputerPrincipal where T : IComputerPrincipal
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

		#region Methods

		public override void TransferQueryFilter(T queryFilter)
		{
			base.TransferQueryFilter(queryFilter);

			if(this.ServicePrincipalNames.Any())
			{
				foreach(var servicePrincipalName in this.ServicePrincipalNames)
				{
					queryFilter.ServicePrincipalNames.Add(servicePrincipalName);
				}
			}
		}

		#endregion
	}
}