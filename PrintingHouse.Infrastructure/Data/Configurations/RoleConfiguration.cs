using Microsoft.AspNetCore.Identity;
using PrintingHouse.Infrastructure.Data.Entities;

namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(CreateRoles());
        }

        private List<IdentityRole> CreateRoles()
        {
            return new List<IdentityRole>() 
            {
                new IdentityRole()
                {
                    Id = "ea294c36-937b-4371-97d8-017af1b46a50",
                    ConcurrencyStamp = "9751c691-0b4c-4c73-861a-ca479b830b12",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                 new IdentityRole()
                {
                    Id = "02fecd1c-f81c-4155-a877-918ef001a7ac",
                    ConcurrencyStamp = "4a329e87-482f-4f51-b6f3-9199132fc15e",
                    Name = "Merchant",
                    NormalizedName = "MERCHANT"
                },
                  new IdentityRole()
                {
                    Id = "945c53a3-952b-4b8a-9b6f-272abe9adbe3",
                    ConcurrencyStamp = "0f5471fe-a7a0-4ea0-80ea-6505377c42f8",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                }
            };


        }
    }
}
