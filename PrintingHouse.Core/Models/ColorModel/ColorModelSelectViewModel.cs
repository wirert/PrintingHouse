namespace PrintingHouse.Core.Models.ColorModel
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    public class ColorModelSelectViewModel
    {
        public int Id { get; set; }
               
        public string Name { get; set; } = null!;
    }
}
