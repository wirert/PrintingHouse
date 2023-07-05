namespace PrintingHouse.Core.AdminModels.Employee
{
    using System.ComponentModel.DataAnnotations;

    public class AllEmployeeViewModel
    {
        public string Id { get; init; } = null!;

        [Display(Name = "Employee Number")]
        public int EmployeeNumber { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; init; } = null!;

        public string Position { get; set; } = null!;

        public string Access { get; set; } = null!;

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = null!;
    }
}
