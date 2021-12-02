using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class Hotel
    {
        public Hotel()
        {
            Reservation = new HashSet<Reservation>();
        }
        public int IDHotel { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Stars { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
