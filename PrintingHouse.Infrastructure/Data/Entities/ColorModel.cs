using PrintingHouse.Infrastructure.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouse.Infrastructure.Data.Entities
{
    public class ColorModel
    {
        public ColorModel()
        {
            Machines = new HashSet<Machine>();
            Articles = new HashSet<Article>();
        }

        [Key] 
        public int Id { get; set; }

        [Required]
        public ColorModelType Name { get; set; }

        public ICollection<Machine> Machines { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
