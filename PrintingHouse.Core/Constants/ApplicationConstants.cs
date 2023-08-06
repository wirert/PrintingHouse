namespace PrintingHouse.Core.Constants
{
    public static class ApplicationConstants
    {
        public const string FullNameClaim = "FullName";

        public const string AdminRoleName = "Admin";
        public const string EmployeeRoleName = "Employee";
        public const string MerchantRoleName = "Merchant";
        public const string PrinterRoleName = "Printer";

        public const string AdminAreaName = "Admin";

        public const string ArticlesCacheKey = "ArticlesCacheKey";
        public const int ArticlesCacheExpirationMinutes = 10;

        public const string MachinesCacheKey = "MachinesCacheKey";
        public const int MachinesCacheExpirationHours = 10;
    }
}
