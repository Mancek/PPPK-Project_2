using System.Windows;
using System.Windows.Controls;
using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for ListHotelsPage.xaml
    /// </summary>
    public partial class ListHotelsPage : BaseHotelsPage
    {
        public ListHotelsPage(GenericViewModel<Hotel> viewModel) : base(viewModel)
        {
            InitializeComponent();
            LvHotels.ItemsSource = viewModel.Items;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new EditHotelPage(ViewModel) { Frame = Frame });

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvHotels.SelectedItem != null)
            {
                Frame.Navigate(new EditHotelPage(ViewModel, LvHotels.SelectedItem as Hotel) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvHotels.SelectedItem != null)
            {
                ViewModel.Items.Remove(LvHotels.SelectedItem as Hotel);
            }
        }
    }
}
