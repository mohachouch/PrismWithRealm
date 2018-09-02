using PrismWithRealm.Domains.Interventions.Business;
using PrismWithRealm.Domains.Interventions.Dal;
using PrismWithRealm.Domains.Interventions.Entities;
using PrismWithRealm.Domains.Interventions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrismWithRealm.Domains.Interventions.Services
{
	public class InterventionService : IInterventionService
	{
		private readonly IInterventionDal interventionDal;

		public InterventionService(IInterventionDal interventionDal)
		{
			this.interventionDal = interventionDal;
		}
		
		public List<InterventionBo> GetInterventionsByDate(DateTime dateTime)
		{
			var interventions = interventionDal.GetQueryable()
				.Where(x => x.DateIntervention == dateTime)
				.ToList()
				.Select(x => InterventionBo.ToBo(x));

			return interventions.ToList();
		}
	}
}
