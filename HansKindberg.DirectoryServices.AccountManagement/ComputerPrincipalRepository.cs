using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using HansKindberg.DirectoryServices.AccountManagement.QueryFilters;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	public class ComputerPrincipalRepository : ComputerPrincipalRepository<ComputerPrincipal, IComputerPrincipal, IComputerPrincipalQueryFilter>, IComputerPrincipalRepository
	{
		#region Constructors

		public ComputerPrincipalRepository(IPrincipalContext principalContext) : base(principalContext) {}

		#endregion
	}

	[SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
	public abstract class ComputerPrincipalRepository<T, TInterface, TQueryFilter> : AuthenticablePrincipalRepository<T, TInterface, TQueryFilter> where T : ComputerPrincipal where TInterface : IComputerPrincipal where TQueryFilter : class, IComputerPrincipalQueryFilter
	{
		#region Constructors

		protected ComputerPrincipalRepository(IPrincipalContext principalContext) : base(principalContext) {}

		#endregion

		#region Methods

		protected internal override IEnumerable<TInterface> CastSearchResult(System.DirectoryServices.AccountManagement.PrincipalSearchResult<Principal> concreteSearchResult)
		{
			if(concreteSearchResult == null)
				throw new ArgumentNullException("concreteSearchResult");

			return concreteSearchResult.Select(item => (TInterface) (IComputerPrincipal) (ComputerPrincipalWrapper) item);
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller. ")]
		protected internal override T CreateConcreteQueryFilter(TQueryFilter queryFilter)
		{
			var computerPrincipal = (T) new ComputerPrincipal(this.CreateConcretePrincipalContext());

			this.PopulateConcreteQueryFilter(computerPrincipal, queryFilter);

			return computerPrincipal;
		}

		protected internal override void PopulateConcreteQueryFilter(T concreteQueryFilter, TQueryFilter queryFilter)
		{
			base.PopulateConcreteQueryFilter(concreteQueryFilter, queryFilter);

			foreach(var servicePrincipalName in queryFilter.ServicePrincipalNames)
			{
				concreteQueryFilter.ServicePrincipalNames.Add(servicePrincipalName);
			}
		}

		#endregion
	}
}