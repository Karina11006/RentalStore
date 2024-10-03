using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Domain.Models
{
    public class RentalDetail
    {
        public int RentalDetailId { get; set; }
        public int Count { get; set; }
        public int EquipmentId { get; set; }
        public Rental Rental { get; set; }
        public Equipment Equipment { get; set; }
    }
}
