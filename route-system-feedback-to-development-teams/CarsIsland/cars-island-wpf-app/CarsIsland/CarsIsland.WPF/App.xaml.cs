using CarsIsland.WPF.Core.Config;
using CarsIsland.WPF.Core.Services;
using CarsIsland.WPF.Core.Services.Interfaces;
using CarsIsland.WPF.Core.ViewModels;
using CarsIsland.WPF.Services;
using CarsIsland.WPF.Views;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CarsIsland.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void RegisterDependencies()
        {
            SimpleIoc.Default.Register<ICarsDataService, CarsDataService>();
            SimpleIoc.Default.Register<IContactsDataService, ContactsDataService>();
            SimpleIoc.Default.Register<ILocationsDataService, LocationsDataService>();

            SimpleIoc.Default.Register<StartPageViewModel>(true);
            SimpleIoc.Default.Register<CarsPageViewModel>();
            SimpleIoc.Default.Register<ContactsPageViewModel>();
            SimpleIoc.Default.Register<LocationsPageViewModel>();
            SimpleIoc.Default.Register<CarDetailsPageViewModel>();
        }

        private void InitializeNavigation()
        {
            INavigationService navigationService;

            if (!SimpleIoc.Default.IsRegistered<INavigationService>())
            {
                // Setup navigation service:
                navigationService = new NavigationService();

                // Configure pages:
                navigationService.Configure(AppViews.StartView, typeof(StartPage));
                navigationService.Configure(AppViews.CarsView, typeof(CarsPage));
                navigationService.Configure(AppViews.CarDetailsView, typeof(CarDetailsWindow));
                navigationService.Configure(AppViews.ContactsView, typeof(ContactsPage));
                navigationService.Configure(AppViews.LocationsView, typeof(LocationsPage));

                // Register NavigationService in IoC container:
                SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializeNavigation();
            RegisterDependencies();
        }
    }
}
