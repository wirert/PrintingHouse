namespace PrintingHouse.Infrastructure.Data.Configurations
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;

    using Entities.Account;


    internal class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(p => p.IsActive).HasDefaultValue(true);

            builder.HasData(CreateUsers());
        }

        private List<ApplicationUser> CreateUsers()
        {
            var users = new List<ApplicationUser>();
            var hasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser()
            {
                Id = Guid.Parse("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                UserName = "Admin123",
                NormalizedUserName = "ADMIN123",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                FirstName = "Admin",
                LastName = "Petrov",
                SecurityStamp = "d2ecdcca-b1e6-4015-aaa1-17c22a17e6b3"
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "123456");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = Guid.Parse("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                UserName = "Merchant1",
                NormalizedUserName = "MERCHANT1",
                Email = "merchant1@mail.com",
                NormalizedEmail = "MERCHANT1@MAIL.COM",
                FirstName = "Merchant",
                LastName = "Georgiev",
                SecurityStamp = "ff91b260-0ab1-48c3-b7dd-ecb740dfce74"
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "123456");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = Guid.Parse("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                UserName = "Employee1",
                NormalizedUserName = "EMPLOYEE1",
                Email = "empl1@mail.com",
                NormalizedEmail = "EMPL1@MAIL.COM",
                FirstName = "Empl",
                LastName = "Nikolov",
                SecurityStamp = "455036d5-b858-4330-83bb-d9bbe1e7d7a0"
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "123456");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = Guid.Parse("ab1c2588-4ee2-408f-a302-fbddfd8ec1b8"),
                UserName = "Printer1",
                NormalizedUserName = "PRINTER1",
                Email = "printer1@mail.com",
                NormalizedEmail = "PRINTER1@MAIL.COM",
                FirstName = "Printer",
                LastName = "Georgiev",
                SecurityStamp = "636d473a-af8e-4b21-b069-02f511f4be73"
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "123456");

            users.Add(user);

            return users;
        }
    }
}
