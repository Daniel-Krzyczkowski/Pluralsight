using CarsIsland.WPF.Core.Config;
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
    public class CarsPageViewModel : AppViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ICarsDataService _carsDataService;

        private bool _isCarSelected;
        public bool IsCarSelected
        {
            get
            {
                return _isCarSelected;
            }

            set
            {
                _isCarSelected = value;
                RaisePropertyChanged(nameof(IsCarSelected));
            }
        }

        public ObservableCollection<Car> _carsList;
        public ObservableCollection<Car> CarsList
        {
            get
            {
                return _carsList;
            }

            set
            {
                _carsList = value;
                RaisePropertyChanged(nameof(CarsList));
            }
        }

        private RelayCommand<Car> _selectCarCommand;
        public RelayCommand<Car> SelectCarCommand
        {
            get
            {
                if (_selectCarCommand == null)
                {
                    _selectCarCommand = new RelayCommand<Car>((car) =>
                    {
                        IsCarSelected = false;
                        _navigationService.NavigateTo(AppViews.CarDetailsView, car);
                    });
                }

                return _selectCarCommand;
            }
        }

        public CarsPageViewModel(INavigationService navigationService,
                         ICarsDataService carsDataService)
        {
            _navigationService = navigationService;
            _carsDataService = carsDataService;
        }

        public async Task GetCarsData()
        {
            var carsData = await _carsDataService.GetAvailableCars();
            CarsList = new ObservableCollection<Car>(carsData);
        }
    }
}
