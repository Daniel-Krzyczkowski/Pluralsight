using CarsIsland.WPF.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsIsland.WPF.Core.ViewModels
{
    public class CarDetailsPageViewModel : AppViewModel
    {
        private Car _selectedCar;

        public Car SelectedCar
        {
            get
            {
                return _selectedCar;
            }

            set
            {
                _selectedCar = value;
                RaisePropertyChanged(nameof(SelectedCar));
            }
        }
    }
}
