using EverydayPower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EverydayPower.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        public ActionResult Index()
        {
            Get_Post_List list = new Get_Post_List(); ;
            var session = Session["username"];
            if (session != null)
            {
                ViewBag.Login = "valid";
            }
            else
            {
                ViewBag.Login = "invalid";
            }
            return View( list);
        }
        [Route("about-us")]
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

        public ActionResult Login(Get_Post_List model)
        {
            Services.Post_Service services = new Services.Post_Service();
            Get_Post_List list = new Get_Post_List();
            var session = Session["username"];
            if (session==null)
            {
                 list = services.LoginService(model.UserDetails);
                if (list.message == "valid")
                {
                    Session["username"] = model.UserDetails.username;
                }
            }
            else
            {
                list.message = "valid";
            }
            ViewBag.User = Session["username"];
            ViewBag.Login = list.message;
            return RedirectToAction("Index"  );
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

    }
}