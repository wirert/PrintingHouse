namespace PrintingHouse.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

    using static Constants.DataConstants.ColorModel;

    [Comment("Color model")]
    public class ColorModel
    {
        public ColorModel()
        {
            Machines = new HashSet<Machine>();
            Articles = new HashSet<Article>();
            Colors = new HashSet<Color>();
        }

        [Comment("Primary key")]
        [Key] 
        public int Id { get; set; }

        [Comment("Color model name")]
        [Required]
        [MaxLength(MaxNameLenght)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Machine> Machines { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<Color> Colors { get; set; }


    }
}
