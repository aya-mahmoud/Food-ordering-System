using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Project.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual List<Product>? Products { get; set; }

    }
}
