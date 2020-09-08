using EverydayPower.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace EverydayPower.Controllers
{
    public class Custom_PostController : Controller
    {
        Services.File_Service service = new Services.File_Service();
        [Route("Upload")]
        [HttpPost]
        public JsonResult FileUpload(HttpPostedFileBase[] ProfileImage)
        {
            FileSave_Model response;
            try
            {
                FileSave_Model model = new FileSave_Model();
                HttpPostedFileBase file = ProfileImage[0];
                if (file.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string date = DateTime.UtcNow.ToString("dd/mm/yyyy");
                    string extention = Path.GetExtension(file.FileName);
                    string FileName = fileName.Replace(" ", "-");
                    string newFileName = date + "-" + FileName + extention;

                    //Get Upload path from Web.Config file AppSettings.  
                    string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();

                    //Its Create complete path to store in server.  
                    model.imagePath = Server.MapPath( UploadPath ) + newFileName;

                    //To copy and save file into server.  
                    file.SaveAs(model.imagePath);

                    model.message = "success";
                    model.imagePath = UploadPath + newFileName;
                    model.imageName = newFileName;

                     response = new FileSave_Model( service.FileSave(model));
                }
                else
                {
                    response = new FileSave_Model();
                    response.message = "EmptyFile";
                }
            }
            catch (Exception ex)
            {
                response = new FileSave_Model();
                response.message = "PostDataError";
            }
            
            return(Json(response ,JsonRequestBehavior.AllowGet));
        }
        // GET: Custom_Post
        public ActionResult Index()
        {
            return View("CustomPostIndex" , new Get_Post_List());
        }

        [Route("Create-Post")]
        public ActionResult NewPost()
        {
            return View("CustomPostIndex", new Get_Post_List());
        }
       

        // GET: Custom_Post/Details/5
        [Route("Quotes/{id}")]
        public ActionResult Show_Post(string id)
        {
            Services.Post_Service services = new Services.Post_Service();
            Get_Post_List list = services.PostGetContent(id);
            var session = Session["username"];
            if (session != null)
            {
                ViewBag.User = session.ToString();
            }
                //ViewBag.Title = list.list[0].seoTitle;
                return View("Show_Post" ,list);
        }

        [HttpPost]
        public JsonResult Update(PostEdit postobj)
        {
            string status = "";
            Services.Post_Service postService = new Services.Post_Service();
            if (ModelState.IsValid)
            {
                status = postService.Update(postobj);
            }
            else
            {
                status = "PostDataError";
            }
            return (Json(status, JsonRequestBehavior.AllowGet));
        }

        [Route("side-links/{start}/{pageIndex}")]
        public JsonResult GetSideLinks(int start ,int pageIndex)
        {
            Services.Post_Service services = new Services.Post_Service();
            return (Json(services.PostGetLatestPosts(start, pageIndex), JsonRequestBehavior.AllowGet));
        }

        // GET: Custom_Post/Create

        [HttpPost]
        public JsonResult Post(Post_Model postobj)
        {
            string status = "";
            Services.Post_Service postService = new Services.Post_Service();
            if (ModelState.IsValid)
            {
                status=postService.PostSave(postobj);
            }
            else
            {
                status = "PostDataError";
            }
            return (Json(status, JsonRequestBehavior.AllowGet));
        }

        // POST: Custom_Post/Create
        [HttpGet]
        public JsonResult GetAll()
        {
            Services.Post_Service services = new Services.Post_Service();
            return (Json(services.PostGetAll(), JsonRequestBehavior.AllowGet));
        }

        [Route("GetByCategoryId/{id}")]
        [HttpGet]
        public JsonResult GetByCategoryId( int id )
        {
            Services.Post_Service services = new Services.Post_Service();
            return (Json(services.PostGetByCategoryId(id), JsonRequestBehavior.AllowGet));
        }

        [HttpGet]
        public JsonResult GetCategoryList()
        {
            Services.Post_Service services = new Services.Post_Service();
            return (Json(services.GetCategoryList(), JsonRequestBehavior.AllowGet));
        }


        // GET: Custom_Post/Edit/5
        [Route("Quotes/Delete/{id}")]
        public ActionResult Delete(string id)
        {
            Services.Post_Service services = new Services.Post_Service();
            Get_Post_List list = services.PostDeleteContent(id);
            //ViewBag.Title = list.list[0].seoTitle;
            return RedirectToAction("Index", "Home" );
        }

        [Route("Quotes/Edit/{id}")]
        public ActionResult Edit(string id) 
        {
            try
            {
                Services.Post_Service services = new Services.Post_Service();
                Get_Post_List list = services.PostGetContent(id);
                // TODO: Add update logic here
                return View("CustomPostIndex", list);

            }
            catch
            {
                return View();
            }
        }

        // GET: Custom_Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Custom_Post/Delete/5
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
