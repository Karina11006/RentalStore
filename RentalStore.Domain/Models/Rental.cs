using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalStore.Domain.Models
{
    public enum RentalStatus
    {
        Active,
        Completed,
        Canceled,
        Overdue
    }
    public class Rental
    {
        public int RentalId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public RentalStatus Status { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public decimal Total { get; set; }
        public ICollection<RentalDetail> Details { get; set; } = new List<RentalDetail>();

    }
}
