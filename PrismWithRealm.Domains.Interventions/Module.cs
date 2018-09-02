using Prism.Ioc;
using PrismWithRealm.Common.Services;
using PrismWithRealm.Domains.Interventions.Dal;
using PrismWithRealm.Domains.Interventions.Entities;
using PrismWithRealm.Domains.Interventions.Interfaces;
using PrismWithRealm.Domains.Interventions.Services;
using Realms;
using System;

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
			database.HasData<Intervention>(
				new Intervention() { Id = 1, Libelle = "Intervention 1", DateIntervention = DateTime.Today },
				//new Intervention() { Id = 2, Libelle = "Intervention 2", DateIntervention = DateTime.Today },
				new Intervention() { Id = 3, Libelle = "Intervention 3", DateIntervention = DateTime.Today.AddDays(1) }
			);


		}
	}
}
