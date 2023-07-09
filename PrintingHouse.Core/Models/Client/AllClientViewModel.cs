namespace PrintingHouse.Core.Models.Client
{
    using System.ComponentModel.DataAnnotations;

    public class AllClientViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = null!;
       
        public string Email { get; set; } = null!;

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = null!;

        [Display(Name = "Merchant")]
        public string MerchantName { get; set; } = null!;

        public int Articles { get; set; }
    }
}
