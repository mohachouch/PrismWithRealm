using Prism;
using Prism.Ioc;
using PrismWithRealm.ViewModels;
using PrismWithRealm.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using Realms;
using PrismWithRealm.Interfaces;
using PrismWithRealm.Common.Services;
using Unity.Injection;
using Prism.Modularity;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PrismWithRealm
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/ViewA");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
			containerRegistry.Register<IRealmManager, RealmManager>();
			containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
        }

		protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
		{
			base.ConfigureModuleCatalog(moduleCatalog);
			moduleCatalog.AddModule<Domains.Interventions.Module>("DomainsInterventionsModule");
			moduleCatalog.AddModule<Bundles.Interventions.Module>("BundlesInterventionsModule");
		}
	}
}

