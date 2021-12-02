using System.Windows;
using System.Windows.Controls;
using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for ListPersonsPage.xaml
    /// </summary>
    /// 
    public partial class ListPeoplePage : BasePeoplePage
    {
        public ListPeoplePage(GenericViewModel<Person> viewModel) : base(viewModel)
        {
            InitializeComponent();
            LvUsers.ItemsSource = viewModel.Items;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new EditPersonPage(ViewModel) { Frame = Frame });

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvUsers.SelectedItem != null)
            {
                Frame.Navigate(new EditPersonPage(ViewModel, LvUsers.SelectedItem as Person) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvUsers.SelectedItem != null)
            {
                ViewModel.Items.Remove(LvUsers.SelectedItem as Person);
            }
        }
    }
}
