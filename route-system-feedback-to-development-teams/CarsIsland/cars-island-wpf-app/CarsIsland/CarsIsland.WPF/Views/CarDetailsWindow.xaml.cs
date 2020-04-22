using CarsIsland.WPF.Core.ViewModels;
using CarsIsland.WPF.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarsIsland.WPF.Views
{
    /// <summary>
    /// Interaction logic for CarDetailsWindow.xaml
    /// </summary>
    public partial class CarDetailsWindow : Window
    {
        public CarDetailsWindow(Car selectedCar)
        {
            InitializeComponent();
            ((CarDetailsPageViewModel)DataContext).SelectedCar = selectedCar;
        }
    }
}
