using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labb3enSida.Model
{
    public interface IBooking
    {
        string Name { get; set; }
        string Day { get; set; }
        string Time { get; set; }
        string TableNum { get; set; }
    }
}
