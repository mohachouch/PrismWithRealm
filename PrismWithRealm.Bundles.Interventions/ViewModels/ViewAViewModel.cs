using Prism.Navigation;
using PrismWithRealm.Common.Bases;
using PrismWithRealm.Common.Services;
using PrismWithRealm.Domains.Interventions.Business;
using PrismWithRealm.Domains.Interventions.Dal;
using PrismWithRealm.Domains.Interventions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismWithRealm.Bundles.Interventions.ViewModels
{
    public class ViewAViewModel : ViewModelBase
    {
		private readonly IRealmManager realmManager;
		private readonly IInterventionService interventionService;
		
		private List<InterventionBo> _interventions;
		public List<InterventionBo> Interventions
		{
			get { return _interventions; }
			set { SetProperty(ref _interventions, value); }
		}

		public ViewAViewModel(INavigationService navigationService, 
			IRealmManager realmManager,
			IInterventionService interventionService)
			: base(navigationService)
        { 
            Title = "Interventions : ";
			this.realmManager = realmManager;
			this.interventionService = interventionService;
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);

			realmManager.Migrate();


			this.Interventions = interventionService.GetInterventionsByDate(DateTime.Today);
			this.Title += Interventions.Count();
		}
	}
}
