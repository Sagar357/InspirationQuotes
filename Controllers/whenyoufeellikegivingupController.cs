using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EverydayPower.Controllers
{
    public class whenyoufeellikegivingupController : Controller
    {
        // GET: whenyoufeellikegivingup
        public ActionResult Index()
        {
            return View("whenyoufeellikegivingup");
        }

        // GET: whenyoufeellikegivingup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: whenyoufeellikegivingup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: whenyoufeellikegivingup/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: whenyoufeellikegivingup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: whenyoufeellikegivingup/Edit/5
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

        // GET: whenyoufeellikegivingup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: whenyoufeellikegivingup/Delete/5
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
