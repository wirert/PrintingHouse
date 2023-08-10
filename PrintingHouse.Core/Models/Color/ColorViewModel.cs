namespace PrintingHouse.Core.Models.Color
{
    public class ColorViewModel
    {
        public int Id { get; set; }
               
        public string Name { get; set; } = null!;
                
        public int InStock { get; set; }
                
        public decimal Price { get; set; }

        public string ColorModel { get; set; } = null!;
    }
}
