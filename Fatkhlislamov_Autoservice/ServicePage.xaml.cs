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

namespace Fatkhlislamov_Autoservice
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            var currentService = FatkhlislamovServiceEntities.GetContext().Service.ToList();

            ServiceListView.ItemsSource = currentService;

            ComboType.SelectedIndex = 0;

            UpdateServices();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }
        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }
        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void UpdateServices()
        {
            var currentService = FatkhlislamovServiceEntities.GetContext().Service.ToList();

            if (ComboType.SelectedIndex == 0) { 
                    currentService = currentService.Where(p => (Convert.ToInt32(p.Discount) >= 0 && Convert.ToInt32(p.Discount) <= 100)).ToList();
                }

            if (ComboType.SelectedIndex == 1)
            {
                currentService = currentService.Where(p => (p.Discount * 100 >= 0 && p.Discount * 100 < 5)).ToList();
            }

            if (ComboType.SelectedIndex == 2)
            {
                currentService = currentService.Where(p => (p.Discount * 100 >= 5 && p.Discount * 100 < 15)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentService = currentService.Where(p => (p.Discount * 100 >= 15 && p.Discount * 100 < 30)).ToList();
            }
            if (ComboType.SelectedIndex == 4)
            {
                currentService = currentService.Where(p => (p.Discount * 100 >= 30 && p.Discount * 100 < 70)).ToList();
            }
            if (ComboType.SelectedIndex == 5)
            {
                currentService = currentService.Where(p => (p.Discount * 100 >= 70 && p.Discount * 100 < 100)).ToList();
            }

            currentService = currentService.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            ServiceListView.ItemsSource = currentService.ToList();

            if (RButtonDown.IsChecked.Value)
            {
                ServiceListView.ItemsSource = currentService.OrderByDescending((p) => p.Discount).ToList();
            }

            if (RButtonUp.IsChecked.Value)
            {
                ServiceListView.ItemsSource = currentService.OrderBy(p => p.Cost).ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());

        }
        
    }
}
