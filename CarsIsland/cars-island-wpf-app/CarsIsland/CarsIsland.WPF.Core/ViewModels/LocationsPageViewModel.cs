using CarsIsland.WPF.Core.Services.Interfaces;
using CarsIsland.WPF.Data.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CarsIsland.WPF.Core.ViewModels
{
    public class LocationsPageViewModel : AppViewModel
    {
        private readonly ILocationsDataService _locationsDataService;

        public ObservableCollection<Location> _locationsList;
        public ObservableCollection<Location> LocationsList
        {
            get
            {
                return _locationsList;
            }

            set
            {
                _locationsList = value;
                RaisePropertyChanged(nameof(LocationsList));
            }
        }

        private RelayCommand<Contact> _selectedContactPerson;
        public RelayCommand<Contact> SelectedContactPerson
        {
            get
            {
                if (_selectedContactPerson == null)
                {
                    _selectedContactPerson = new RelayCommand<Contact>((contactPerson) =>
                    {

                    });
                }

                return _selectedContactPerson;
            }
        }

        public LocationsPageViewModel(ILocationsDataService locationsDataService)
        {
            _locationsDataService = locationsDataService;
        }

        public async Task GetLocationsData()
        {
            var locationsData = await _locationsDataService.GetLocations();
            LocationsList = new ObservableCollection<Location>(locationsData);
        }
    }
}
