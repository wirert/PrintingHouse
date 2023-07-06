namespace PrintingHouse.Core.AdminModels.Employee
{
    using System.ComponentModel.DataAnnotations;

    using ApplicationUser;
    using Position;

    public class AddEmployeeViewModel
    {
        [Required]
        public Guid ApplicationUserId { get; set; }

        [Required]        
        public int PositionId { get; set; }

        [Required]
        public string Role { get; set; } = null!;

        public IEnumerable<AllPositionViewModel> Positions { get; set; } = Enumerable.Empty<AllPositionViewModel>();

        public IEnumerable<string> AccessLevels { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<AllUsersViewModel> Users { get; set; } = Enumerable.Empty<AllUsersViewModel>();
    }
}
