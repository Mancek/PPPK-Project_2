using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class Reservation
    {
        public int IDReservation { get; set; }
        public int HotelID { get; set; }
        public int PersonID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
