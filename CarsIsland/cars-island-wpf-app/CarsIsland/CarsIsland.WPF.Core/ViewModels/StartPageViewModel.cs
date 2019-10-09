using CarsIsland.WPF.Core.Config;
using CarsIsland.WPF.Core.Services.Interfaces;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsIsland.WPF.Core.ViewModels
{
    public class StartPageViewModel : AppViewModel
    {
       private readonly INavigationService _navigationService;


        private RelayCommand _navigateToCarsViewCommand;
        public RelayCommand NavigateToCarsViewCommand
        {
            get
            {
                if (_navigateToCarsViewCommand == null)
                {
                    _navigateToCarsViewCommand = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo(AppViews.CarsView);
                    });
                }

                return _navigateToCarsViewCommand;
            }
        }

        private RelayCommand _navigateToContactsViewCommand;
        public RelayCommand NavigateToContactsViewCommand
        {
            get
            {
                if (_navigateToContactsViewCommand == null)
                {
                    _navigateToContactsViewCommand = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo(AppViews.ContactsView);
                    });
                }

                return _navigateToContactsViewCommand;
            }
        }

        private RelayCommand _navigateToLocationsViewCommand;
        public RelayCommand NavigateToLocationsViewCommand
        {
            get
            {
                if (_navigateToLocationsViewCommand == null)
                {
                    _navigateToLocationsViewCommand = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo(AppViews.LocationsView);
                    });
                }

                return _navigateToLocationsViewCommand;
            }
        }

        public StartPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
