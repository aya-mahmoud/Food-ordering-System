using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Project.Models
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        public int TotalPrice { get; set; }
        public DeliveryStatus? Status { get; set; }

        public virtual List<Order>? Orders { get; set; }

        public virtual List<Product>? Products { get; set; }

        public virtual User? User { get; set; }

        [ForeignKey("User")]
        public int? UserID { get; set; }



    }
}
