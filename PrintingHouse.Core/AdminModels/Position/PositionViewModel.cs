namespace PrintingHouse.Core.AdminModels.Position
{
    using System.ComponentModel.DataAnnotations;

    public class PositionViewModel : AddPositionViewModel
    {
        [Key]
        public int Id { get; set; }       
    }
}
