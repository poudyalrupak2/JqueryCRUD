
using Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Domain.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ExpendatureDbContext context;
        public AuthRepository(ExpendatureDbContext context)
        {
            this.context = context;
        }
        public bool IsUserExists(string Email)
        {
          var user=  context.Login.Where(x=>x.Email==Email);


            if(user.Count()>0){
                return true;
            }
            else
            {
                return false;

            }
        }
        
        public  Login Login(string Email, string Password)
        {
            var user = context.Login.FirstOrDefault(a => a.Email == Email);
            if(user==null)
            {
                return null;
            }
            else
            {
                if (user.PasswordHash != null && user.PasswordSalt != null)
                {
                    if (!VerifyPasswordHash(Password, user.PasswordHash, user.PasswordSalt))
                    {
                        return null;
                    }
                    else
                    {
                        return user;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passWordHash, byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var code = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i=0; i<code.Length;i++)
                {
                    if(code[i]!=passWordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //Password Change
        public  Login Registor(Login Login, string password)
        {
            byte[] passwordhash, passwordsalt;
            createpasswordhash(password, out passwordhash,out passwordsalt);
            var logindata = context.Login.FirstOrDefault(m => m.Email == Login.Email);
            logindata.PasswordSalt = passwordhash;
            logindata.RandomPass = "";
            logindata.PasswordSalt = passwordsalt;
            context.Login.Attach(logindata);

            context.Entry(logindata).Property(x => x.PasswordHash).IsModified = true;
            context.Entry(logindata).Property(x => x.PasswordSalt).IsModified = true;
            context.Entry(logindata).Property(x => x.RandomPass).IsModified = true;


            context.SaveChanges();
            return Login;
        }
        //same ho mathiko
        public Login registers(Login Login,string password)
        {
            byte[] passwordhash, passwordsalt;
            createpasswordhash(password, out passwordhash, out passwordsalt);
            var logindata = context.Login.FirstOrDefault(m => m.Email == Login.Email);
            logindata.PasswordHash = passwordhash;
            logindata.RandomPass = "";
            logindata.PasswordSalt = passwordsalt;
            context.Login.Attach(logindata);

            context.Entry(logindata).Property(x => x.PasswordHash).IsModified = true;
            context.Entry(logindata).Property(x => x.PasswordSalt).IsModified = true;
            context.Entry(logindata).Property(x => x.RandomPass).IsModified = true;
            context.SaveChanges();

            return Login;
        }

        private void createpasswordhash(string password, out byte[] passwordhash, out byte[] passwordsalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
    }
}