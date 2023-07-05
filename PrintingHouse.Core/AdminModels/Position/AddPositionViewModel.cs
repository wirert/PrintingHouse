namespace PrintingHouse.Core.AdminModels.Position
{
    using System.ComponentModel.DataAnnotations;

    using static Infrastructure.Constants.DataConstants.Position;

    public class AddPositionViewModel
    {
        [Required]
        [StringLength(MaxPositionLenght, MinimumLength = MinPositionLenght)]
        public string Name { get; set; } = null!;
    }
}
