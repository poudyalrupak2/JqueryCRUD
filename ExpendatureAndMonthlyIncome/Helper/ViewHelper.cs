using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpendatureAndMonthlyIncome.Helper
{
    public class ViewHelper
    {
      
            public static string RenderRazorViewToString(Controller controller, string viewName, object model = null)
            {
                controller.ViewData.Model = model;
                using (var sw = new StringWriter())
                {
                    //IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                    //var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                    //                                         viewName);
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                    ViewPage viewPage = new ViewPage();

                    ViewContext viewContext = new ViewContext(
                        controller.ControllerContext,
                        viewResult.View,
                        controller.ViewData,
                        controller.TempData,
                        sw

                    );
                    viewResult.View.Render(viewContext, sw);
                    return sw.GetStringBuilder().ToString();
                }
            }

        public static string RenderRazorViewToString1(Controller controller, string viewName, IEnumerable<Object> model  )
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                //IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                //var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                //                                         viewName);
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                ViewPage viewPage = new ViewPage();

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw

                );
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

    }
}




//public class Helper
//{

//    public static string RenderRazorViewToString(Controller controller, string viewName, object model = null)
//    {
//        controller.ViewData.Model = model;
//        using (var sw = new StringWriter())
//        {
//            IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
//            ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

//            ViewContext viewContext = new ViewContext(
//                controller.ControllerContext,
//                viewResult.View,
//                controller.ViewData,
//                controller.TempData,
//                sw,
//                new HtmlHelperOptions()
//            );
//            viewResult.View.RenderAsync(viewContext);
//            return sw.GetStringBuilder().ToString();
//        }
//    }

//}