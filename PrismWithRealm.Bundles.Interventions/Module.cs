using Prism.Ioc;
using Prism.Modularity;
using PrismWithRealm.Bundles.Interventions.Views;
using PrismWithRealm.Bundles.Interventions.ViewModels;

namespace PrismWithRealm.Bundles.Interventions
{
    public class Module : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

		public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();
        }
    }
}
