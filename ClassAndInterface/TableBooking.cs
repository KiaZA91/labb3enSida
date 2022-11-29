using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labb3enSida.Model
{
    internal class Booking : IBooking
    {
        public string Name { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string TableNum { get; set; }

        public Booking(string day, string name, string time, string tableNum)
        {
            Day = day;
            Name = name;
            Time = time;
            TableNum = tableNum;
        }
    }
}
