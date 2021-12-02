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
    /// Interaction logic for EditHotelPage.xaml
    /// </summary>
    public partial class EditHotelPage : BaseHotelsPage
    {
        private readonly Hotel hotel;
        public EditHotelPage(GenericViewModel<Hotel> viewModel, Hotel hotel = null) : base(viewModel)
        {
            InitializeComponent();
            this.hotel = hotel ?? new Hotel();
            DataContext = hotel;
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                hotel.Stars = int.Parse(TbStars.Text.Trim());
                hotel.Address = TbAddress.Text.Trim();
                hotel.City = TbCity.Text.Trim();
                if (hotel.IDHotel == 0)
                {
                    ViewModel.Items.Add(hotel);
                }
                else
                {
                    ViewModel.Update(hotel);
                }
                Frame.NavigationService.GoBack();
            }
        }

        private bool FormValid()
        {
            bool valid = true;
            GridContainter.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                if (string.IsNullOrEmpty(e.Text.Trim())
                    || ("Int".Equals(e.Tag) && !int.TryParse(e.Text, out int stars)))
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
