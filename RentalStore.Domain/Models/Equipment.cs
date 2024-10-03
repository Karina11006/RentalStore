using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Domain.Models
{
    public enum Condition
    {
        New,
        Used
    }
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }   
        public bool Availability { get; set; }
        public Condition Condition { get; set; }
        public int QuantityInStock { get; set; }
        public decimal PricePerDay { get; set; }
        public Category Category { get; set; }
        public string ImageUrl { get; set; } = "/images/no-image-icon.png";
        public ICollection<Rental> Rentals { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
