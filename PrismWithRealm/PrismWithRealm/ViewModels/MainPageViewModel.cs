using Prism.Navigation;
using PrismWithRealm.Common.Bases;
using PrismWithRealm.Interfaces;
using Realms;
using System.Linq;
using System.Threading;

namespace PrismWithRealm.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, 
			IDatabaseContext context) 
            : base (navigationService)
        {
            Title = "Main Page";
		}
	}
}
