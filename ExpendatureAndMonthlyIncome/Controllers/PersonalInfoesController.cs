using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Domain.Models;
using Domains.Data;

namespace ExpendatureAndMonthlyIncome.Controllers
{
    public class PersonalInfoesController : Controller
    {
        private ExpendatureDbContext db = new ExpendatureDbContext();

        // GET: PersonalInfoes
        public ActionResult Index()
        {
            return View(db.personalInfos.ToList());
        }

        // GET: PersonalInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInfo personalInfo = db.personalInfos.Find(id);
            if (personalInfo == null)
            {
                return HttpNotFound();
            }
            return View(personalInfo);
        }

        // GET: PersonalInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonalInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,CompanyName")] PersonalInfo personalInfo)
        {
            if (ModelState.IsValid)
            {

                Random generator = new Random();
                string password = generator.Next(0, 999999).ToString("D6");



                var message = new MailMessage();
                message.To.Add(new MailAddress(personalInfo.Email));
                message.Subject = "Account has been created";
                message.Body = string.Format("Use this email and {0} this Password to login:", password);
                using (var smtp = new SmtpClient())
                {
                    try
                    {

                        smtp.Send(message);
                        db.Login.Add(new Domain.Login
                        {
                            Email = personalInfo.Email,
                            Role = "User",
                            RandomPass = password,
                            Status = true

                        });

                        TempData["Message"] = "User Created Successfully.";


                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", "unable to send mail");
                        return View(personalInfo);
                        //return new HttpStatusCodeResult(HttpStatusCode.RequestTimeout);
                    }
                }

                db.personalInfos.Add(personalInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personalInfo);
        }

        // GET: PersonalInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInfo personalInfo = db.personalInfos.Find(id);
            if (personalInfo == null)
            {
                return HttpNotFound();
            }
            return View(personalInfo);
        }

        // POST: PersonalInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,CompanyName")] PersonalInfo personalInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personalInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personalInfo);
        }

        // GET: PersonalInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInfo personalInfo = db.personalInfos.Find(id);
            if (personalInfo == null)
            {
                return HttpNotFound();
            }
            return View(personalInfo);
        }

        // POST: PersonalInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonalInfo personalInfo = db.personalInfos.Find(id);
            db.personalInfos.Remove(personalInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
