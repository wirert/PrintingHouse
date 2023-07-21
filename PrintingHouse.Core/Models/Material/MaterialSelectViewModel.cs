namespace PrintingHouse.Core.Models.Material
{
    using PrintingHouse.Infrastructure.Data.Entities.Enums;

    public class MaterialSelectViewModel
    {        
        public int Id { get; set; }
       
        public string Type { get; set; } = null!;

        public MeasureUnit? MeasureUnit { get; set; }
    }
}
