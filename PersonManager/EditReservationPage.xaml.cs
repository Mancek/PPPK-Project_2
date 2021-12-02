using System;
using System.Collections.Generic;
using System.Globalization;
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
using Zadatak.Dal;
using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for EditReservationPage.xaml
    /// </summary>
    public partial class EditReservationPage : BaseReservationPage
    {
        private readonly Reservation reservation;

        private readonly IList<Hotel> hotels;

        private readonly IList<Person> people = new List<Person>();
        public EditReservationPage(GenericViewModel<Reservation> viewModel, Reservation reservation = null) : base(viewModel)
        {
            InitializeComponent();
            this.reservation = reservation ?? new Reservation();
            DataContext = reservation;
            people = RepositoryFactory.GetRepository().GetList<Person>();
            hotels = RepositoryFactory.GetRepository().GetList<Hotel>();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                reservation.HotelID = int.Parse(TbHotelID.Text.Trim());
                reservation.PersonID = int.Parse(TbPersonID.Text.Trim());
                reservation.FromDate = TbFromDate.Text.Trim();
                reservation.ToDate = TbToDate.Text.Trim();
                if (reservation.IDReservation == 0)
                {
                    ViewModel.Items.Add(reservation);
                }
                else
                {
                    ViewModel.Update(reservation);
                }
                Frame.NavigationService.GoBack();
            }
        }

        private bool FormValid()
        {
            bool valid = true;
            Hotel hotel = hotels.FirstOrDefault(h => h.IDHotel.Equals(int.Parse(TbHotelID.Text)));
            GridContainter.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                if (string.IsNullOrEmpty(e.Text.Trim())
                    || ("Int".Equals(e.Tag) && !int.TryParse(e.Text, out int id))
                    || ("DateTime".Equals(e.Tag) && !DateTime.TryParse(e.Text, out DateTime date))
                    || ("ForeignKeyPerson".Equals(e.Tag) && people.FirstOrDefault(p => p.IDPerson.Equals(int.Parse(e.Text))) == null)
                    || ("ForeignKeyHotel".Equals(e.Tag) && hotels.FirstOrDefault(h => h.IDHotel.Equals(int.Parse(e.Text))) == null))
                {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                }
                else
                {
                    e.Background = Brushes.White;
                }
            });
            return valid;
        }
    }
}
