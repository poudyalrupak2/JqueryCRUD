using Domain.Models;
using Domains.Data;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Implementation
{
   public  class UnitOfWork
    {
        private ExpendatureDbContext context = new ExpendatureDbContext();
        private GenericRepository<Income> Income;
        private GenericRepository<PersonalInfo> PersonalInfo;
        private GenericRepository<Product> Product;
        private GenericRepository<ProductData> Productdata;

        public GenericRepository<Income> IncomeRepository
        {
            get
            {

                if (this.Income == null)
                {
                    this.Income = new GenericRepository<Income>(context);
                }
                return Income;
            }
        }

        public GenericRepository<PersonalInfo> PersonalInfoRepository
        {
            get
            {

                if (this.PersonalInfo == null)
                {
                    this.PersonalInfo = new GenericRepository<PersonalInfo>(context);
                }
                return PersonalInfo;
            }
        }
        public GenericRepository<Product> product
        {
            get
            {

                if (this.Product == null)
                {
                    this.Product = new GenericRepository<Product>(context);
                }
                return Product;
            }
        }
        public GenericRepository<ProductData> ProductDatas
        {
            get
            {

                if (this.Productdata == null)
                {
                    this.Productdata = new GenericRepository<ProductData>(context);
                }
                return Productdata;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public void Rollback()
        { 
            context.Dispose(); 
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}

