using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Hosting_Equipment
    {
        public int Hosting_id { get; set; }
        public virtual Hosting Hosting { get; set; }
        public int Equipment_id { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
