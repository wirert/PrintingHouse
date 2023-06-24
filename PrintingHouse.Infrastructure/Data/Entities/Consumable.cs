using Microsoft.EntityFrameworkCore;
using PrintingHouse.Infrastructure.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace PrintingHouse.Infrastructure.Data.Entities
{
    [Comment("Machine consumable")]
    public class Consumable
    {
        public Consumable()
        {
            ArticleConsumables = new List<ArticleConsumable>();
        }

        [Comment("Consumable primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Consumable type (enumeration)")]
        [Required] 
        public ConsumableType Type { get; set; }

        public int MyProperty { get; set; }

        [Comment("Consumable current quantit in stock")]
        [Required]
        public int InStock { get; set; }

        [Comment("Consumable price")]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public ICollection<ArticleConsumable> ArticleConsumables { get; set; }

    }
}
