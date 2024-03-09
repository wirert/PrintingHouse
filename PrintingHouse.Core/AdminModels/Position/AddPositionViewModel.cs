namespace PrintingHouse.Core.AdminModels.Position
{
    using System.ComponentModel.DataAnnotations;

    using static Infrastructure.Constants.DataConstants.Position;

    public class AddPositionViewModel
    {
        [Required]
        [StringLength(MaxPositionLength, MinimumLength = MinPositionLength)]
        public string Name { get; set; } = null!;
    }
}
