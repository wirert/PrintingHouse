namespace PrintingHouse.Core.Models.Material
{
    public class MaterialViewModel : MaterialSelectViewModel
    {
        public double Width { get; set; }
                
        public double Lenght { get; set; }
                
        public decimal Price { get; set; }
               
        public int InStock { get; set; }
    }
}
