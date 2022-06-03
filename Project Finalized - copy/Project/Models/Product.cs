using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Project.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public int Price { get; set; }
        public string? Category { get; set; }

        [AllowHtml]
        //public string Contents { get; set; }
        public byte[]? Image { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
        public string? PhotoSrc { get; set; }

    }
}
