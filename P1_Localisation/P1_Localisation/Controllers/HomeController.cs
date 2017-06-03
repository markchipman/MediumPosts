using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using P1_Localisation.ActionFilters;

namespace P1_Localisation.Controllers {
    [Localisation]
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = Resources.Resources.key1;

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = Resources.Resources.key2;

            return View();
        }
    }
}