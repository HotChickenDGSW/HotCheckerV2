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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotChecker_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            App.cardViewModel.ChangeScreenEventHandler += CardViewModel_ChangeScreenEventHandler;
            App.temperatureViewModel.ChangeScreenEventHandler += TemperatureViewModel_ChangeScreenEventHandler;
        }

        private void TemperatureViewModel_ChangeScreenEventHandler()
        {
            CheckCardView.Visibility = Visibility.Visible;
            CheckTemperatureView.Visibility = Visibility.Collapsed;
        }

        private void CardViewModel_ChangeScreenEventHandler()
        {
            CheckCardView.Visibility = Visibility.Collapsed;
            CheckTemperatureView.Visibility = Visibility.Visible;
        }
    }
}
