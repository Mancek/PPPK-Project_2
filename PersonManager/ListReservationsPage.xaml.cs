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
using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for ListReservationsPage.xaml
    /// </summary>
    public partial class ListReservationsPage : BaseReservationPage
    {
        public ListReservationsPage(GenericViewModel<Reservation> viewModel) : base(viewModel)
        {
            InitializeComponent();
            LvReservations.ItemsSource = viewModel.Items;
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new EditReservationPage(ViewModel) { Frame = Frame });

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvReservations.SelectedItem != null)
            {
                Frame.Navigate(new EditReservationPage(ViewModel, LvReservations.SelectedItem as Reservation) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvReservations.SelectedItem != null)
            {
                ViewModel.Items.Remove(LvReservations.SelectedItem as Reservation);
            }
        }
    }
}
