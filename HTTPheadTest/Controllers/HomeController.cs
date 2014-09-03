using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTPheadTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var qingqiuCount = 120;
            var qingqiu = Request.Headers["Qingqiu"];
            if (!string.IsNullOrEmpty(qingqiu))
            {
                qingqiu = qingqiu.ToLower();
                if (int.TryParse(qingqiu, out qingqiuCount))
                {
                    qingqiu = qingqiuCount.ToString();
                }
                else
                {
                    qingqiu = (qingqiuCount - 1).ToString();
                }
                var response = Response;
                response.AppendHeader("Qingqiu", qingqiu);
            }
            else
            {
                var response = Response;
                response.AppendHeader("Qingqiu", "120");
            }


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}