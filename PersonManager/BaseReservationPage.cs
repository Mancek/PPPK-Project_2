using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak
{
    public class BaseReservationPage : FramedPage<Reservation>
    {
        public BaseReservationPage(GenericViewModel<Reservation> viewModel) : base(viewModel)
        {
        }
    }
}
