using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.SharedKernel.Dto
{
    public class CartItemDto
    {
        public EquipmentDto Equipment { get; set; }
        public int Quantity { get; set; }
    }
}
