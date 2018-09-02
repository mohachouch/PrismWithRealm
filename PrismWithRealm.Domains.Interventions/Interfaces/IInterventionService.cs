using PrismWithRealm.Domains.Interventions.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrismWithRealm.Domains.Interventions.Interfaces
{
    public interface IInterventionService
    {
		List<InterventionBo> GetInterventionsByDate(DateTime dateTime);
    }
}
