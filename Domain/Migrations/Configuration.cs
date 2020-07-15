namespace Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Domains.Data.ExpendatureDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Domains.Data.ExpendatureDbContext context)
        {

            byte[] passwordhash;
            byte[] passwordsalt;
            byte[] passwordhash1;
            byte[] passwordsalt1;
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                string password = "nepalnepal1";
                passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordsalt = hmac.Key;
                string password1 = "nepalnepal";
                passwordhash1 = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password1));
                passwordsalt1 = hmac.Key;
            }
            context.Login.AddOrUpdate(x => x.Id,
           new Login
           {
               Id = 1,
               Email = "poudyalrupak2@gmail.com",
               Role = "Admin",
               PasswordHash = passwordhash,
               PasswordSalt = passwordsalt,
               Status = true
           },
             new Login
             {
                 Id = 2,
                 Email = "abc@gmail.com",
                 Role = "User",
                 PasswordHash = passwordhash,
                 PasswordSalt = passwordsalt,
                 Status = true
             },

            new Login
            {
                Id = 2,
                Email = "dbug@gmail.com",
                Role = "Admin",
                PasswordHash = passwordhash1,
                PasswordSalt = passwordsalt1,
                Status = true
            });

            context.SaveChanges();

        }
    }
}
