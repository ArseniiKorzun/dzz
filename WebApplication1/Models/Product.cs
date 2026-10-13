using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Вкажіть назву")]
        public string Name { get; set; } = "";

        [Range(0.01, 1_000_000, ErrorMessage = "Ціна має бути від 0.01 до 1 000 000")]
        public decimal Price { get; set; }
    }
}
