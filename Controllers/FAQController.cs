using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EverydayPower.Models;

namespace EverydayPower.Controllers
{
    public class FAQController : Controller
    {
        // GET: FAQ
        //[Route("FAQ")]
        //public ActionResult FAQ()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        // GET: FAQ/Details/5
        [Route("FAQ")]
        public ActionResult FAQ()
        {
            validate();
            Get_Post_List list = null;
            Services.Post_Service postService = new Services.Post_Service();
            if (ModelState.IsValid)
            {
                list = new Get_Post_List();
                list.FaqList = postService.FaqGetAll();
            }
            else
            {
                list.message = "PostDataError";
            }
          
            return View("FAQ" ,list);
        }

        // GET: FAQ/Create
        public ActionResult Create()
        {
            return View();
        }

        private void validate()
        {
            var session = Session["username"];
            if (session != null)
            {
                ViewBag.Login = "valid";
            }
            else
            {
                ViewBag.Login = "invalid";
            }
        }

        // POST: FAQ/Create
        [Route("Create")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                string status = "";
                Services.Post_Service postService = new Services.Post_Service();
                if (ModelState.IsValid)
                {
                    Faq_model model = new Faq_model();
                    model.answer = Request.Form["answer"].ToString();
                    model.questions = Request.Form["query"].ToString();
                    status = postService.FaqSave(model);
                }
                else
                {
                    status = "PostDataError";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FAQ/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FAQ/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FAQ/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FAQ/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
