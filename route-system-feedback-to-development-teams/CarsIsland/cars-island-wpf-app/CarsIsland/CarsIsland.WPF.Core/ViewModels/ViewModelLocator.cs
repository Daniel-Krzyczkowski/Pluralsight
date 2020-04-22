using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsIsland.WPF.Core.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
        }

        public StartPageViewModel StartPageViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<StartPageViewModel>();
            }
        }

        public CarsPageViewModel CarsPageViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<CarsPageViewModel>();
            }
        }

        public ContactsPageViewModel ContactsPageViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<ContactsPageViewModel>();
            }
        }

        public LocationsPageViewModel LocationsPageViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<LocationsPageViewModel>();
            }
        }

        public CarDetailsPageViewModel CarDetailsPageViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<CarDetailsPageViewModel>();
            }
        }
    }
}
