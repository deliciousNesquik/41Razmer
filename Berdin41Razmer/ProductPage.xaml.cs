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
        string CountObjectAll;
        public ProductPage(User user)
        {
            InitializeComponent();

            var currentProduct = Berdin41SizeEntities.GetContext().Product.ToList();
            ProductListView.ItemsSource = currentProduct;
            CountObjectAll = Convert.ToString(currentProduct.Count);
            CountObject.Text = "Кол-во " + Convert.ToString(currentProduct.Count) + " из " + CountObjectAll;

            DiscountFilter.SelectedIndex = 0;
            TBoxSearch.Text = "";

            if(user == null)
            {
                authuser.Text = "Вы авторизованы как гость";
                authuserrole.Text = "Роль: Гость";
            }
            else
            {
                authuser.Text = "Вы авторизованы как " + user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
                switch (user.UserRole)
                {
                    case 2:
                        authuserrole.Text = "Роль: " + "Клиент"; break;
                    case 3:
                        authuserrole.Text = "Роль: " + "Менеджер"; break;
                    case 1:
                        authuserrole.Text = "Роль: " + "Администратор"; break;
                    default:
                        break;
                }
            }
        }

        public void Update()
        {
            var currentProduct = Berdin41SizeEntities.GetContext().Product.ToList();
            currentProduct = currentProduct.Where(p => (p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower()))).ToList();

            if (UpCost.IsChecked.Value)
            {
                currentProduct = currentProduct.OrderBy(p => p.ProductCost).ToList();
            }
            if (DownCost.IsChecked.Value)
            {
                currentProduct = currentProduct.OrderByDescending(p => p.ProductCost).ToList();
            }
            if (DiscountFilter.SelectedIndex == 1)
            {
                currentProduct = currentProduct.Where(p => Int32.Parse(p.SkidkaDeystv) > 0 && Int32.Parse(p.SkidkaDeystv) <= 9.99).ToList();
            }
            if (DiscountFilter.SelectedIndex == 2)
            {
                currentProduct = currentProduct.Where(p => Int32.Parse(p.SkidkaDeystv) > 10 && Int32.Parse(p.SkidkaDeystv) <= 14.99).ToList();
            }
            if (DiscountFilter.SelectedIndex == 3)
            {
                currentProduct = currentProduct.Where(p => Int32.Parse(p.SkidkaDeystv) > 15).ToList();
            }

            CountObject.Text = "Кол-во " + (Convert.ToString(currentProduct.Count)) + " из " + CountObjectAll;
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
