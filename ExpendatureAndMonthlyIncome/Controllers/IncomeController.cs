using BookProject.Filters;
using Domain.Data.Implementation;
using Domains.Models;
using ExpendatureAndMonthlyIncome.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace ExpendatureAndMonthlyIncome.Controllers
{
    [SessionCheck]
    public class IncomeController : Controller
    {
        private readonly UnitOfWork unitOfWork=new UnitOfWork();
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult Showproduct() {
            var s = unitOfWork.IncomeRepository.GetAll();

            return View(s);
        }
        public JsonResult Product()
        {
            var s = unitOfWork.IncomeRepository.GetAll();
            return Json(new { html = ViewHelper.RenderRazorViewToString1(this, "Product", s) }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Create() {
            return PartialView(); 
        }
        public JsonResult CreateIncome()
        {
            Income income = new Income();
            return Json(new { html = ViewHelper.RenderRazorViewToString(this, "Create",income) }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Create (Income income){
        
        if(ModelState.IsValid)
            {
                try
                {
                    income.Id = Guid.NewGuid();
                    unitOfWork.IncomeRepository.Insert(income);
                    unitOfWork.Save();
                    var s = unitOfWork.IncomeRepository.GetAll();

                    return Json(new {isvalid=true });

                }
                catch(Exception ex)
                {
                    TempData["error"] = "Error saving";
                    unitOfWork.Rollback();
                    return Json(new { isvalid = false, html = ViewHelper.RenderRazorViewToString(this, "create") });


                }
            }
            return Json(new { isvalid = false, html = ViewHelper.RenderRazorViewToString(this, "create") });

        }

        public ActionResult Edit(Guid id) 
        {
            if (id != null)
            {
                Income income = unitOfWork.IncomeRepository.GetById(id);
                if (income == null)
                {
                    return HttpNotFound();
                }
                return PartialView(income);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
        public JsonResult EditIncome(Guid id)
        {
            Income income = new Income();

            if (id != null)
            {
                 income = unitOfWork.IncomeRepository.GetById(id);
                if (income == null)
                {
                    return Json(false);
                }
                return Json(new { html = ViewHelper.RenderRazorViewToString(this, "Edit", income) }, JsonRequestBehavior.AllowGet);
            }
            return Json(false);

        }






        [HttpPost]
        public JsonResult Edit(Income income)
        {
           if(ModelState.IsValid)
            {
                try
                {
                    unitOfWork.IncomeRepository.Update(income);
                    unitOfWork.Save();
                    var s = unitOfWork.IncomeRepository.GetAll();

                    return Json(new { isvalid = true, html = ViewHelper.RenderRazorViewToString1(this, "Index",s) });

                }
                catch (Exception ex)
                {
                    TempData["error"] = "Error saving";

                    unitOfWork.Rollback();
                    return Json(new { isvalid = false, html = ViewHelper.RenderRazorViewToString(this, "Edit") });


                }

            }
            return Json(new { isvalid = false, html = ViewHelper.RenderRazorViewToString(this, "Edit") });

        }
        public ActionResult Delete(int id) {

            unitOfWork.IncomeRepository.Delete(id);
            unitOfWork.Save();
            return View("Index");
        
        }

    }
}