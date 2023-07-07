namespace PrintingHouse.Core.AdminModels.Employee
{
    using System.ComponentModel.DataAnnotations;

    using Position;

    public class EditEmployeeViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid ApplicationUserId { get; set; }

        public string? FullName { get; set; }

        [Required]
        public int PositionId { get; set; }

        public string? PositionName { get; set; }

        public string? OldPositionName { get; set; }

        public string? Role { get; set; }

        public IEnumerable<PositionViewModel> Positions { get; set; } = Enumerable.Empty<PositionViewModel>();

        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();        
    }
}
