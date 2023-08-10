namespace PrintingHouse.ViewComponents.Models
{
    public class ConsumablesViewModel
    {
        public string MaterialName { get; set; } = null!;

        public int MaterialQuantityInStock { get; set; }

        public IEnumerable<ConsumableColorViewModel> Colors { get; set; } = new List<ConsumableColorViewModel>();
    }
}
