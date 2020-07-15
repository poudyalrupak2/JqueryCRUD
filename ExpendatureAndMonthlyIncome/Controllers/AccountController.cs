
using BookProject.Viewmodel;
using Domain;
using Domain.Data;
using Domains.Data;
using ExpendatureAndMonthlyIncome;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LabProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly ExpendatureDbContext context = new ExpendatureDbContext();
        private IAuthRepository auth;
        public AccountController()
        {
            this.auth = new AuthRepository(context);
        }
        // GET: Admin/Admins



        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }
        Role role = new Role();
        [HttpPost]
        public ActionResult Login(LoginViewModel l, string ReturnUrl)
            {
            try
            {
                ViewBag.ReturnUrl = ReturnUrl;
                if (auth.IsUserExists(l.Email))
                {
                    var login = auth.Login(l.Email, l.Password);

                    string pass = context.Login.FirstOrDefault(a => (a.Email == l.Email)).RandomPass;
                    if (login != null)
                    {
                        if (login.Status == true)
                        {
                            var Admin = context.Login.FirstOrDefault(a => (a.Email == l.Email));


                           
                            Session.Add("userEmail", Admin.Email);
                            Session.Add("category", Admin.Role);
                            var objAdmin = context.Login.FirstOrDefault(a => (a.Email == l.Email));
                            FormsAuthentication.SetAuthCookie(l.Email, false);
                            string[] roles = role.GetRolesForUser(objAdmin.Email);
                            if (roles.Contains("SuperAdmin"))
                            {
                                return RedirectToAction("Index", "Admins");
                            }
                            if (roles.Contains("Admin"))
                            {
                                Session.Add("id", Admin.Id);

                                return RedirectToAction("AdminIndex", "Dashboard");
                            }
                            if (roles.Contains("User"))
                            {
                                //string image=context.Admins.Where(m => m.Email == Admin.Email).FirstOrDefault().Image;
                                //Session.Add("image",image);
                                //string name = context.UserDetails.Where(m => m.Email == Admin.Email).FirstOrDefault().LabName;
                                //string LabNo = context.UserDetails.Where(m => m.Email == Admin.Email).FirstOrDefault().LabNo;
                                //string address = context.UserDetails.Where(m => m.Email == Admin.Email).FirstOrDefault().Address;
                                //string district = context.UserDetails.Where(m => m.Email == Admin.Email).FirstOrDefault().District;
                                //int id = context.UserDetails.Where(m => m.Email == Admin.Email).FirstOrDefault().Id;

                                //Session.Add("id", id);
                                //Session.Add("Name", name);
                                //Session.Add("LobNo", LabNo);
                                //Session.Add("Address", address);
                                //Session.Add("district", district);
                                //Admin.LastLogin = DateTime.Now.ToString();
                                //context.Entry(Admin).Property(x => x.LastLogin).IsModified = true;
                                //return RedirectToAction("ClientIndex", "Dashboard");

                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Admin has not accepted your request");
                            return View();
                        }
                    }
                    else if (l.Password == pass)
                    {
                        TempData["message"] = l.Email;
                        return RedirectToAction("NewPassword");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Password");
                    }
                }
                ModelState.AddModelError("", "Invalid User");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());

            }

            return View();


        }

        public ActionResult NewPassword()
        {

            return PartialView();

        }
        [HttpPost]
        public ActionResult NewPassword(PasswordConform pass)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string message = TempData["message"].ToString();
                    var query = (from q in context.Login
                                 where q.Email == message
                                 select q).First();
                    string password = pass.Password;
                    Login login = auth.registers(query, password);
                    //query.RandomPass = null;

                    return RedirectToAction("Login");



                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return PartialView();
        }
        public ActionResult ForgetPassword() {
            return PartialView();

        }
       [HttpPost]
        public ActionResult ForgetPassword(string email) {
           
            var Admin = context.Login.FirstOrDefault(a => (a.Email == email));
            if(Admin!=null)
            {
                Random generator = new Random();
               String password = generator.Next(0, 999999).ToString("D6");
                var message = new MailMessage();
                  message.To.Add(new MailAddress(Admin.Email));
                message.Subject = "Forget password";
                message.Body = "Use this Password to login:" + password;
                using (var smtp = new SmtpClient())
                {
                    try
                    {

                        smtp.Send(message);
                        Admin.RandomPass = password;
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", "unable to send email");
                        return PartialView();
}
                }

                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "this email doesnot exist");
            return PartialView();

        }
        public ActionResult Logout()
        {


            FormsAuthentication.SignOut();

            Session.Abandon();
            return RedirectToAction("Login");


        }
    }
}