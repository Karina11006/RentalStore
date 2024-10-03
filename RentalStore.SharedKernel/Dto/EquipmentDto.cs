using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.SharedKernel.Dto
{
    public enum Condition
    {
        New,
        Used       
    }
    public class EquipmentDto
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }   
        public bool Availability { get; set; }
        public Condition Condition { get; set; }
        public int QuantityInStock { get; set; }
        public float PricePerDay { get; set; }
        public string ImageUrl { get; set; }

    }
}
