using CarsIsland.WPF.Core.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsIsland.WPF.Core.Services.Interfaces
{
    public interface INavigationService
    {
        void Configure(AppViews viewKey, Type viewType);
        void InitializeRootView(object rootView);
        void NavigateTo(AppViews viewKey);
        void NavigateTo(AppViews viewKey, object parameter);
    }
}
