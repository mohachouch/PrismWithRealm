using Prism.Ioc;
using PrismWithRealm.Common.Services;
using PrismWithRealm.Domains.Interventions.Dal;
using PrismWithRealm.Domains.Interventions.Entities;
using PrismWithRealm.Domains.Interventions.Interfaces;
using PrismWithRealm.Domains.Interventions.Services;
using Realms;
using System;
using System.Linq;

namespace PrismWithRealm.Domains.Interventions
{
    public class Module : RealmModule
	{
		public override void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
			// Dal
			containerRegistry.Register<IInterventionDal, InterventionDal>();

			// Services
			containerRegistry.Register<IInterventionService, InterventionService>();
		}

		public override void OnDataBaseCreated(Realm database)
		{
			var lieu1 = new Lieu() { Id = 1, Libelle = "L'Isle D'Abeau" };
			var lieu2 = new Lieu() { Id = 2, Libelle = "Villefontaine" };
			var lieu3 = new Lieu() { Id = 3, Libelle = "Bourgoin Jallieu" };

			database.HasData<Lieu>(
				lieu1,
				lieu2, 
				lieu3
			);

			database.HasData<Intervention>(
				new Intervention() { Id = 1, Libelle = "Intervention 1", DateIntervention = DateTime.Today, Lieu = lieu3 },
				new Intervention() { Id = 2, Libelle = "Intervention 2", DateIntervention = DateTime.Today, Lieu = lieu2 },
				new Intervention() { Id = 3, Libelle = "Intervention 3", DateIntervention = DateTime.Today.AddDays(1), Lieu = lieu1 }
			);
		}
	}
}
