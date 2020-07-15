using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data
{
   public interface IAuthRepository
    {
        Login Registor(Login Login, string password);
       Login Login(string Email, string Password);
        bool IsUserExists(string Email);
        Login registers(Login Login, string password);
    }
}
