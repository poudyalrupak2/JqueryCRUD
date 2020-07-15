using Domain;
using Domain.Models;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;

namespace Domains.Data
{
    public class ExpendatureDbContext: DbContext
    {
        public ExpendatureDbContext():base("defaultconnection")
        {


        }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductData> Products { get; set; }
        public DbSet<Income> Income { get; set; }
        public DbSet<Login>  Login { get; set; }
        public  DbSet<PersonalInfo> personalInfos { get; set; }
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }


        private void AddTimestamps()
        {
            try {
                var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

                var currentUsername = HttpContext.Current.Session["userEmail"].ToString();

                foreach (var entity in entities)
                {
                    if (entity.State == EntityState.Added)
                    {
                        ((BaseEntity)entity.Entity).DateCreated = DateTime.UtcNow;
                        ((BaseEntity)entity.Entity).UserCreated = currentUsername;
                    }
                    else
                    {

                        ((BaseEntity)entity.Entity).DateModified = DateTime.UtcNow;
                        ((BaseEntity)entity.Entity).UserModified = currentUsername;
                    }
                }
            }
            catch(Exception)
            {

            }
            }
       


    }
}
