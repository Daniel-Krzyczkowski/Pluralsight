using CarsIsland.WPF.Core.Config;
using CarsIsland.WPF.Core.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsIsland.WPF.Core.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        public INavigationService NavigationService => SimpleIoc.Default.GetInstance<INavigationService>();

        public void NavigationInvoked(AppViews viewKey)
        {
            NavigationService.NavigateTo(viewKey);
        }

        public void NavigationInvoked(AppViews viewKey, object parameter)
        {
            NavigationService.NavigateTo(viewKey, parameter);
        }
    }
}
