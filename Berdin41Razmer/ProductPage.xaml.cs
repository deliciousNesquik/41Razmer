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

namespace Berdin41Razmer
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();

            var currentProduct = Berdin41SizeEntities.GetContext().Product.ToList();
            ProductListView.ItemsSource = currentProduct;

            DiscountFilter.SelectedIndex = 0;
            TBoxSearch.Text = "";


        }

        public void Update()
        {
            var currentProduct = Berdin41SizeEntities.GetContext().Product.ToList();

            if (UpCost.IsChecked.Value)
            {
                currentProduct = currentProduct.OrderBy(p => p.ProductCost).ToList();
            }
            if (DownCost.IsChecked.Value)
            {
                currentProduct = currentProduct.OrderByDescending(p => p.ProductCost).ToList();
            }

            ProductListView.ItemsSource = currentProduct;
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void DiscountFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void UpCost_Checked(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void DownCost_Checked(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
