using PrismWithRealm.Domains.Interventions.Entities;
using System;

namespace PrismWithRealm.Domains.Interventions.Business
{
    public class InterventionBo 
    {
		public long Id { get; set; }

		public string Libelle { get; set; }

		public DateTime DateIntervention { get; set; }

		public static InterventionBo ToBo(Intervention intervention)
		{
			InterventionBo interventionBo = new InterventionBo
			{
				Id = intervention.Id,
				Libelle = intervention.Libelle,
				DateIntervention = intervention.DateIntervention.DateTime
			};

			return interventionBo;
		}


		public static Intervention ToEntity(InterventionBo interventionBo)
		{
			Intervention intervention = new Intervention
			{
				Id = interventionBo.Id,
				Libelle = interventionBo.Libelle,
				DateIntervention = interventionBo.DateIntervention
			};

			return intervention;
		}
	}
}
