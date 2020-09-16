using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Totallydays.Models
{
    public class Hosting_Equipment
    {
        public int HostingHosting_id { get; set; }
        public virtual Hosting Hosting { get; set; }

        public int EquipmentEquipment_id { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
