using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverydayPower.Models
{
    public class Post_Model
    {
        public string fileAttachmentCode { get; set; }
        public string postContent { get; set; }
        public string postIconImage { get; set; }
        public string postIconImagePath { get; set; }
        public string postUrl { get; set; }
        public int postId { get; set; }
        public string category { get; set; }
        public string seoUrl { get; set; }
        public string categoryName { get; set; }
        public string shortName { get; set; }
        public string seoTitle { get; set; }
        public string seoDescription { get; set; }

    }

    public class PostEdit
    {
        public string seoUrl { get; set; }
        public string postContent { get; set; }

    }

    public class Login_model
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class Get_Post_List
    {
        public Get_Post_List()
        {
            this.list = new List<Post_Model>();
        }
        public List<Post_Model> list { get; set; }
        public string message { get; set; }
        public Login_model UserDetails { get; set; }
        public Faq_model_list FaqList { get; set; }
    }

    public class Category_Model
    {

        public int categoryId { get; set; }
        public string categoryDesc { get; set; }
        public string categoryName { get; set; }

    }

    public class Get_Category_List
    {
        public Get_Category_List()
        {
            this.list = new List<Category_Model>();
        }
        public List<Category_Model> list { get; set; }
        public string message { get; set; }
    }

    public class Search_Model
    {
        public string searchStr { get; set; }
    }
}