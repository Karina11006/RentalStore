using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.SharedKernel.Dto
{
    public class FeedbackDto
    {
        public int FeedbackId { get; set; }
        public int EquipmentId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } 
        public DateTime FeedbackDate { get; set; }
        public EquipmentDto Equipment { get; set; }
    }
}
