using System.Windows;
using System.Windows.Controls;
using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string BtnPrefix = "Btn";
        public MainWindow()
        {
            InitializeComponent();
            Frame.Navigate(new ListPeoplePage(new GenericViewModel<Person>()) { Frame = Frame });
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            switch (button.Name)
            {
                case BtnPrefix + nameof(Person):
                    Frame.Navigate(new ListPeoplePage(new GenericViewModel<Person>()) { Frame = Frame });
                    break;
                case BtnPrefix + nameof(Hotel):
                    Frame.Navigate(new ListHotelsPage(new GenericViewModel<Hotel>()) { Frame = Frame });
                    break;
                case BtnPrefix + nameof(Reservation):
                    Frame.Navigate(new ListReservationsPage(new GenericViewModel<Reservation>()) { Frame = Frame });
                    break;
                default:
                    break;
            }
        }
    }
}
