using Domain.Data.Implementation;
using Domain.viewmodel;
using Domains.Models;
using ExpendatureAndMonthlyIncome.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ExpendatureAndMonthlyIncome.Controllers
{
    public class ProductDataController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();
        // GET: ProductData
        public ActionResult Index()
        {
            productVM productvm = new productVM();
           // var a=unitOfWork.product.GetAll(includeProperties: "ProductData");

            productvm.productDatas = unitOfWork.ProductDatas.GetAll().ToList();
            return View(productvm);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            Guid a = Guid.NewGuid();
            product.ProductData.Id = a;
            unitOfWork.ProductDatas.Insert(product.ProductData);
            unitOfWork.Save();
            
            unitOfWork.product.Insert(product);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public ActionResult CreateProduct()
        {
            return PartialView();
        }
        public ActionResult ProductDetail(Guid id)
        {
            var a = unitOfWork.product.GetAll(filter: m => m.ProductDataId == id);
            return PartialView();
        }
        public JsonResult ProductDetails(Guid id)
        {
            var a = unitOfWork.product.GetAll(filter: m => m.ProductDataId == id);
            return Json(new { html = ViewHelper.RenderRazorViewToString1(this, "ProductDetail", a) },JsonRequestBehavior.AllowGet);

        }
        public JsonResult DeleteProduct (int id){
            unitOfWork.product.Delete(id);
            unitOfWork.Save();
            var a = unitOfWork.product.GetAll();
            return Json(new { html = ViewHelper.RenderRazorViewToString1(this, "ProductDetail", a) }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductDataId != null)
                {

                    unitOfWork.product.Insert(product);
                    unitOfWork.Save();
                    return Json(new { isvalid = true });

                }
            }
            return Json(new{ isvalid=false, html=ViewHelper.RenderRazorViewToString(this, "CreateProduct") });
        }
    }
}