using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using static PrintingHouse.Infrastructure.Constants.DataConstants.ColorModel;

namespace PrintingHouse.Infrastructure.Data.Entities
{
    [Comment("Color model")]
    public class ColorModel
    {
        public ColorModel()
        {
            Machines = new HashSet<Machine>();
            Articles = new HashSet<Article>();
        }

        [Comment("Primary key")]
        [Key] 
        public int Id { get; set; }

        [Comment("Color model name")]
        [Required]
        [MaxLength(MaxNameLenght)]
        public string Name { get; set; } = null!;

        public ICollection<Machine> Machines { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
