using EverydayPower.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;

namespace EverydayPower.Services
{
    public class Post_Service
    {
        public string PostSave(Post_Model model)
        {
            string status = "failed";
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("category_name");
                dt.Columns.Add("category_desc");
                int i = 0;
                foreach (var element in model.categoryList)
                {
                    DataRow dr = dt.NewRow();
                    dr["category_name"] = element.categoryName;
                    dr["category_desc"] = element.categoryDesc;
                    dt.Rows.Add(dr);
                }
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "tmp_prc_savePostData";
                    cmd.Parameters.AddWithValue("@post_url", model.postUrl);
                    cmd.Parameters.AddWithValue("@post_content", model.postContent);
                    cmd.Parameters.AddWithValue("@post_icon_image", model.fileAttachmentCode);
                    cmd.Parameters.AddWithValue("@post_category", dt);
                    cmd.Parameters.AddWithValue("@shortname", model.shortName);
                    cmd.Parameters.AddWithValue("@seotitle", model.seoTitle);
                    cmd.Parameters.AddWithValue("@seodesc", model.seoDescription);
                    cmd.Parameters.AddWithValue("@seo_url", model.seoUrl.Replace(" ","-"));

                    cmd.ExecuteNonQuery();
                }
                status = "saved";
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }

        public string FaqSave(Faq_model faq)
        {
            string status = "failed";
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "tmpprcpostfaq";
                    cmd.Parameters.AddWithValue("@question", faq.questions);
                    cmd.Parameters.AddWithValue("@answer", faq.answer);

                    cmd.ExecuteNonQuery();
                }
                status = "saved";
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }


        public string Update(Post_Model model)
        {
            string status = "failed";
            try
            {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("category_name");
                    dt.Columns.Add("category_desc");
                    int i = 0;
                    foreach (var element in model.categoryList)
                    {
                        DataRow dr = dt.NewRow();
                        dr["category_name"] = element.categoryName;
                        dr["category_desc"] = element.categoryDesc;
                        dt.Rows.Add(dr);
                    }
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString()))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "tmp_prc_UpdatePostData";
                        cmd.Parameters.AddWithValue("@post_url", model.postUrl);
                        cmd.Parameters.AddWithValue("@post_content", model.postContent);
                        cmd.Parameters.AddWithValue("@post_icon_image", model.fileAttachmentCode);
                        cmd.Parameters.AddWithValue("@post_category", dt);
                        cmd.Parameters.AddWithValue("@shortname", model.shortName);
                        cmd.Parameters.AddWithValue("@seotitle", model.seoTitle);
                        cmd.Parameters.AddWithValue("@seodesc", model.seoDescription);
                        cmd.Parameters.AddWithValue("@seo_url", model.seoUrl.Replace(" ", "-"));

                        cmd.ExecuteNonQuery();
                    }
                
                status = "Updated";
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }


        public Faq_model_list FaqGetAll()
        {
            string status = "failed";
            Faq_model_list list = new Faq_model_list();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString()))
                {
                    DataSet ds = new DataSet();
                    con.Open();
                    ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "tmpprcgetfaq");
                    foreach (DataTable dt in ds.Tables)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Faq_model obj = new Faq_model();
                            if (!string.IsNullOrEmpty(dr["question"].ToString()))
                            {
                                obj.questions = dr["question"].ToString();
                            }
                            else
                            {
                                obj.questions = "";
                            }
                            if (!string.IsNullOrEmpty(dr["answer"].ToString()))
                            {
                                obj.answer = dr["answer"].ToString();
                            }
                            else
                            {
                                obj.answer = "";
                            }

                            list.list.Add(obj);
                        }
                    }
                }
                list.message = "fetched";
            }
            catch (Exception ex)
            {
                list.message = ex.Message;
            }
            return list;
        }


        public Get_Post_List PostGetAll(int pageIndex)
        {
            string status = "failed";
            Get_Post_List list = new Get_Post_List();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString()))
                {
                    DataSet dset = new DataSet();
                    con.Open();
                    SqlParameter[] param= new SqlParameter[3];
                    dset=SqlHelper.ExecuteDataset(con ,CommandType.StoredProcedure , "tmp_prc_getPostData" ,param );
                    DataTable dtable = Post_Service.SetPgination(dset, pageIndex);

                    using (DataTable dt = dtable)
                    {
                        foreach(DataRow dr in dt.Rows)
                        {
                            Post_Model obj = new Post_Model();
                            if (!string.IsNullOrEmpty(dr["post_id"].ToString()))
                            {
                                obj.postId = Convert.ToInt32(dr["post_id"].ToString());
                            }
                            else
                            {
                                obj.postId = 0;
                            }
                            if (!string.IsNullOrEmpty(dr["post_url"].ToString()))
                            {
                                obj.postUrl = dr["post_url"].ToString();
                            }
                            else
                            {
                                obj.postUrl = "";
                            }
                            if (!string.IsNullOrEmpty(dr["file_name"].ToString()))
                            {
                                obj.postIconImage = dr["file_name"].ToString();
                            }
                            else
                            {
                                obj.postIconImage = "";
                            }
                            if (!string.IsNullOrEmpty(dr["file_path"].ToString()))
                            {
                                obj.postIconImagePath = dr["file_path"].ToString();
                            }
                            else
                            {
                                obj.postIconImagePath = "";
                            }
                            if (!string.IsNullOrEmpty(dr["category_name"].ToString()))
                            {
                                obj.category =  dr["category_name"].ToString();
                            }
                            else
                            {
                                obj.category = "";
                            }
                            if (!string.IsNullOrEmpty(dr["seo_url"].ToString()))
                            {
                                obj.seoUrl = dr["seo_url"].ToString();
                            }
                            else
                            {
                                obj.seoUrl = "";
                            }
                            if (!string.IsNullOrEmpty(dr["post_category"].ToString()))
                            {
                                obj.categoryName = dr["post_category"].ToString();
                            }
                            else
                            {
                                obj.categoryName = "";
                            }
                            list.list.Add(obj);
                        }
                    }

                    using (DataTable dt = dset.Tables[1])
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            PostCount_Model countObj = new PostCount_Model();
                            if (!string.IsNullOrEmpty(dr["category_id"].ToString()))
                            {
                                countObj.categoryId = Convert.ToInt32(dr["category_id"]);
                            }
                            else
                            {
                                countObj.categoryId = 0;
                            }
                            if (!string.IsNullOrEmpty(dr["category"].ToString()))
                            {
                                countObj.category = dr["category"].ToString();
                            }
                            else
                            {
                                countObj.category = "";
                            }
                            if (!string.IsNullOrEmpty(dr["no_of_posts"].ToString()))
                            {
                                countObj.noOfPosts = Convert.ToInt32(dr["no_of_posts"]);
                            }
                            else
                            {
                                countObj.noOfPosts = 0;
                            }
                            list.postCountList.Add(countObj);
                        }
                    }
                }
                list.message = "fetched";
            }
            catch (Exception ex)
            {
                list.message = ex.Message;
            }
            return list;
        }

        private static DataTable SetPgination(DataSet ds ,int pageIndex)
        {
            int row = 1;
            DataTable response = new DataTable();
            DataSet dataset = new DataSet();
            foreach(DataRow dr in ds.Tables[1].Rows)
            {
                DataTable dt = ds.Tables[0].Select("category_name='"+ dr["category"].ToString() + "'" ).AsEnumerable().Skip(pageIndex * 10).Take(10).CopyToDataTable();
                dataset.Tables.Add(dt);
            }
            foreach (DataTable dt in dataset.Tables)
            {
                
                if(row<dataset.Tables.Count)
                dataset.Tables[0].Merge(dataset.Tables[row]);
                row++;
            }
            dataset.Tables[0].DefaultView.Sort="createddate DESC";
            response = dataset.Tables[0];
            return response;
        }

        public Get_Post_List PostGetByCategoryId( int CategoryId )
        {
            string status = "failed";
            Get_Post_List list = new Get_Post_List();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString()))
                {
                    DataSet ds = new DataSet();
                    con.Open();
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@categoryId", CategoryId);
                    param[1] = new SqlParameter("@isAll", 1);
                    ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "tmp_prc_getPostData" ,param);
                    foreach (DataTable dt in ds.Tables)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Post_Model obj = new Post_Model();
                            
                            if (!string.IsNullOrEmpty(dr["post_url"].ToString()))
                            {
                                obj.postUrl = dr["post_url"].ToString();
                            }
                            else
                            {
                                obj.postUrl = "";
                            }
                            
                            if (!string.IsNullOrEmpty(dr["seo_url"].ToString()))
                            {
                                obj.seoUrl = dr["seo_url"].ToString();
                            }
                            else
                            {
                                obj.seoUrl = "";
                            }
                            if (!string.IsNullOrEmpty(dr["shortname"].ToString()))
                            {
                                obj.shortName = dr["shortname"].ToString();
                            }
                            else
                            {
                                obj.shortName = "";
                            }
                            if (!string.IsNullOrEmpty(dr["category_name"].ToString()))
                            {
                                obj.category = dr["category_name"].ToString();
                            }
                            else
                            {
                                obj.category = "";
                            }

                            list.list.Add(obj);
                        }
                    }
                }
                list.message = "fetched";
            }
            catch (Exception ex)
            {
                list.message = ex.Message;
            }
            return list;
        }


        public Get_Category_List GetCategoryList()
        {
            string status = "failed";
            Get_Category_List list = new Get_Category_List();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString()))
                {
                    DataSet ds = new DataSet();
                    con.Open();
                    ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "tmp_prc_getCategoryList");
                    foreach (DataTable dt in ds.Tables)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Category_Model obj = new Category_Model();
                            if (!string.IsNullOrEmpty(dr["category_id"].ToString()))
                            {
                                obj.categoryId = Convert.ToInt32(dr["category_id"].ToString());
                            }
                            else
                            {
                                obj.categoryId = 0;
                            }
                            
                            if (!string.IsNullOrEmpty(dr["category_name"].ToString()))
                            {
                                obj.categoryName = dr["category_name"].ToString();
                            }
                            else
                            {
                                obj.categoryName = "";
                            }
                           
                            if (!string.IsNullOrEmpty(dr["category_desc"].ToString()))
                            {
                                obj.categoryDesc = dr["category_desc"].ToString();
                            }
                            else
                            {
                                obj.categoryDesc = "";
                            }
                            list.list.Add(obj);
                        }
                    }
                }
                list.message = "fetched";
            }
            catch (Exception ex)
            {
                list.message = ex.Message;
            }
            return list;
        }

        public Get_Post_List SearchService(string searchStr)
        {
            string status = "failed";
            Get_Post_List list = new Get_Post_List();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString()))
                {
                    DataSet ds = new DataSet();
                    con.Open();
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@strSearch", searchStr);
                    ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "tmpprcsearch", param);
                    foreach (DataTable dt in ds.Tables)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Post_Model obj = new Post_Model();
                            if (!string.IsNullOrEmpty(dr["post_id"].ToString()))
                            {
                                obj.postId = Convert.ToInt32(dr["post_id"].ToString());
                            }
                            else
                            {
                                obj.postId = 0;
                            }
                            if (!string.IsNullOrEmpty(dr["post_url"].ToString()))
                            {
                                obj.postUrl = dr["post_url"].ToString();
                            }
                            else
                            {
                                obj.postUrl = "";
                            }
                            if (!string.IsNullOrEmpty(dr["file_name"].ToString()))
                            {
                                obj.postIconImage = dr["file_name"].ToString();
                            }
                            else
                            {
                                obj.postIconImage = "";
                            }
                            if (!string.IsNullOrEmpty(dr["file_path"].ToString()))
                            {
                                obj.postIconImagePath = dr["file_path"].ToString();
                            }
                            else
                            {
                                obj.postIconImagePath = "";
                            }
                            if (!string.IsNullOrEmpty(dr["category_name"].ToString()))
                            {
                                obj.category = dr["category_name"].ToString();
                            }
                            else
                            {
                                obj.category = "";
                            }
                            if (!string.IsNullOrEmpty(dr["seo_url"].ToString()))
                            {
                                obj.seoUrl = dr["seo_url"].ToString();
                            }
                            else
                            {
                                obj.seoUrl = "";
                            }
                            if (!string.IsNullOrEmpty(dr["post_category"].ToString()))
                            {
                                obj.categoryName = dr["post_category"].ToString();
                            }
                            else
                            {
                                obj.categoryName = "";
                            }
                            list.list.Add(obj);
                        }
                    }
                }
                list.message = "fetched";
            }
            catch (Exception ex)
            {
                list.message = ex.Message;
            }
            return list;
        }


        public Get_Post_List LoginService(Login_model user)
        {
            string status = "failed";
            Get_Post_List list = new Get_Post_List();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString()))
                {
                    DataSet ds = new DataSet();
                    con.Open();
                    SqlParameter[] param = new SqlParameter[4];
                    param[0] = new SqlParameter("@username", user.username);
                    param[1] = new SqlParameter("@password", user.password);
                    param[2] = new SqlParameter("@IsValid" ,SqlDbType.Int);
                    param[2].Direction = ParameterDirection.Output;
                    ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "tmpprclogin", param);
                    int OutParam = 0;
                    if (!String.IsNullOrEmpty(Convert.ToString(param[2].Value)) )
                    {
                        OutParam = Convert.ToInt32(param[2].Value);
                    }
                    if (OutParam == 1)
                    {
                        list.message = "valid";

                    }
                    else if (OutParam == 0)
                    {
                        list.message = "invalid";

                    }
                }
            }
            catch (Exception ex)
            {
                list.message = ex.Message;
            }
            return list;
        }


        public Get_Post_List PostGetContent(string id)
        {
            string status = "failed";
            Get_Post_List list = new Get_Post_List();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString()))
                {
                    DataSet ds = new DataSet();
                    Post_Model obj = new Post_Model();
                    con.Open();
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@seo_url", id);
                    ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "tmp_prc_getPostData" ,param);
                    using (DataTable dt = ds.Tables[0])
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            //if (!string.IsNullOrEmpty(dr["post_id"].ToString()))
                            //{
                            //    obj.postId = Convert.ToInt32(dr["post_id"].ToString());
                            //}
                            //else
                            //{
                            //    obj.postId = 0;
                            //}
                            if (!string.IsNullOrEmpty(dr["post_url"].ToString()))
                            {
                                obj.postUrl = dr["post_url"].ToString();
                            }
                            else
                            {
                                obj.postUrl = "";
                            }
                            if (!string.IsNullOrEmpty(dr["file_name"].ToString()))
                            {
                                obj.postIconImage = dr["file_name"].ToString();
                            }
                            else
                            {
                                obj.postIconImage = "";
                            }
                            if (!string.IsNullOrEmpty(dr["file_path"].ToString()))
                            {
                                obj.postIconImagePath = dr["file_path"].ToString();
                            }
                            else
                            {
                                obj.postIconImagePath = "";
                            }
                            //if (!string.IsNullOrEmpty(dr["post_category"].ToString()))
                            //{
                            //    obj.category = dr["post_category"].ToString();
                            //}
                            //else
                            //{
                            //    obj.category = "";
                            //}
                            if (!string.IsNullOrEmpty(dr["post_content"].ToString()))
                            {
                                obj.postContent = dr["post_content"].ToString();
                            }
                            else
                            {
                                obj.postContent = "";
                            }
                            if (!string.IsNullOrEmpty(dr["seo_url"].ToString()))
                            {
                                obj.seoUrl = dr["seo_url"].ToString();
                            }
                            else
                            {
                                obj.seoUrl = "";
                            }
                            if (!string.IsNullOrEmpty(dr["seo_title"].ToString()))
                            {
                                obj.seoTitle = dr["seo_title"].ToString();
                            }
                            else
                            {
                                obj.seoTitle = "";
                            }
                            if (!string.IsNullOrEmpty(dr["seo_description"].ToString()))
                            {
                                obj.seoDescription = dr["seo_description"].ToString();
                            }
                            else
                            {
                                obj.seoDescription = "";
                            }
                            if (!string.IsNullOrEmpty(dr["shortname"].ToString()))
                            {
                                obj.shortName = dr["shortname"].ToString();
                            }
                            else
                            {
                                obj.shortName = "";
                            }
                            //if (!string.IsNullOrEmpty(dr["categorycaption"].ToString()))
                            //{
                            //    obj.categoryName = dr["categorycaption"].ToString();
                            //}
                            //else
                            //{
                            //    obj.categoryName = "";
                            //}
                            if (!string.IsNullOrEmpty(dr["file_attachment_code"].ToString()))
                            {
                                obj.fileAttachmentCode = dr["file_attachment_code"].ToString();
                            }
                            else
                            {
                                obj.fileAttachmentCode = "";
                            }
                            
                            list.list.Add(obj);
                        }
                    }

                    using (DataTable dt = ds.Tables[1])
                    {
                        foreach(DataRow dr in dt.Rows)
                        {
                            Category_Model model = new Category_Model();
                            if (!string.IsNullOrEmpty(dr["category_name"].ToString()))
                            {
                                model.categoryName = dr["category_name"].ToString();
                            }
                            else
                            {
                                model.categoryName = "";
                            }
                            if (!string.IsNullOrEmpty(dr["category_desc"].ToString()))
                            {
                                model.categoryDesc = dr["category_desc"].ToString();
                            }
                            else
                            {
                                model.categoryDesc = "";
                            }
                            if (!string.IsNullOrEmpty(dr["category_id"].ToString()))
                            {
                                model.categoryId = Convert.ToInt32(dr["category_id"]);
                            }
                            else
                            {
                                model.categoryId = 0;
                            }
                            list.categoryList.Add(model);
                        }
                    }
                }
                list.message = "fetched";
            }
            catch (Exception ex)
            {
                list.message = ex.Message;
            }
            return list;
        }

        public Get_Post_List PostDeleteContent(string id)
        {
            string status = "failed";
            Get_Post_List list = new Get_Post_List();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString()))
                {
                    DataSet ds = new DataSet();
                    con.Open();
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@seo_url", id);
                    ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "tmp_prc_deletePostData", param);
                    //foreach (DataTable dt in ds.Tables)
                    //{
                    //    foreach (DataRow dr in dt.Rows)
                    //    {
                    //        Post_Model obj = new Post_Model();
                    //        if (!string.IsNullOrEmpty(dr["post_id"].ToString()))
                    //        {
                    //            obj.postId = Convert.ToInt32(dr["post_id"].ToString());
                    //        }
                    //        else
                    //        {
                    //            obj.postId = 0;
                    //        }
                    //        if (!string.IsNullOrEmpty(dr["post_url"].ToString()))
                    //        {
                    //            obj.postUrl = dr["post_url"].ToString();
                    //        }
                    //        else
                    //        {
                    //            obj.postUrl = "";
                    //        }
                    //        if (!string.IsNullOrEmpty(dr["file_name"].ToString()))
                    //        {
                    //            obj.postIconImage = dr["file_name"].ToString();
                    //        }
                    //        else
                    //        {
                    //            obj.postIconImage = "";
                    //        }
                    //        if (!string.IsNullOrEmpty(dr["file_path"].ToString()))
                    //        {
                    //            obj.postIconImagePath = dr["file_path"].ToString();
                    //        }
                    //        else
                    //        {
                    //            obj.postIconImagePath = "";
                    //        }
                    //        if (!string.IsNullOrEmpty(dr["post_category"].ToString()))
                    //        {
                    //            obj.category = dr["post_category"].ToString();
                    //        }
                    //        else
                    //        {
                    //            obj.category = "";
                    //        }
                    //        if (!string.IsNullOrEmpty(dr["post_content"].ToString()))
                    //        {
                    //            obj.postContent = dr["post_content"].ToString();
                    //        }
                    //        else
                    //        {
                    //            obj.postContent = "";
                    //        }
                    //        if (!string.IsNullOrEmpty(dr["seo_url"].ToString()))
                    //        {
                    //            obj.seoUrl = dr["seo_url"].ToString();
                    //        }
                    //        else
                    //        {
                    //            obj.seoUrl = "";
                    //        }
                    //        if (!string.IsNullOrEmpty(dr["seo_title"].ToString()))
                    //        {
                    //            obj.seoTitle = dr["seo_title"].ToString();
                    //        }
                    //        else
                    //        {
                    //            obj.seoTitle = "";
                    //        }
                    //        if (!string.IsNullOrEmpty(dr["seo_description"].ToString()))
                    //        {
                    //            obj.seoDescription = dr["seo_description"].ToString();
                    //        }
                    //        else
                    //        {
                    //            obj.seoDescription = "";
                    //        }
                    //        list.list.Add(obj);
                    //    }
                    //}
                }
                list.message = "Deleted";
            }
            catch (Exception ex)
            {
                list.message = ex.Message;
            }
            return list;
        }


        public Get_Post_List PostGetLatestPosts(int start ,int pageIndex)
        {
            string status = "failed";
            Get_Post_List list = new Get_Post_List();
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@start", start);
                param[1] = new SqlParameter("@pageIndex", pageIndex);
                param[2] = new SqlParameter("@isAll", 1);
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString()))
                {
                    DataSet ds = new DataSet();
                    con.Open();
                    ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "tmp_prc_getPostData", param);
                    foreach (DataTable dt in ds.Tables)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Post_Model obj = new Post_Model();
                            if (!string.IsNullOrEmpty(dr["post_id"].ToString()))
                            {
                                obj.postId = Convert.ToInt32(dr["post_id"].ToString());
                            }
                            else
                            {
                                obj.postId = 0;
                            }
                            if (!string.IsNullOrEmpty(dr["post_url"].ToString()))
                            {
                                obj.postUrl = dr["post_url"].ToString();
                            }
                            else
                            {
                                obj.postUrl = "";
                            }
                           
                            if (!string.IsNullOrEmpty(dr["seo_url"].ToString()))
                            {
                                obj.seoUrl = dr["seo_url"].ToString();
                            }
                            else
                            {
                                obj.seoUrl = "";
                            }
                            if (!string.IsNullOrEmpty(dr["category_name"].ToString()))
                            {
                                obj.category = dr["category_name"].ToString();
                            }
                            else
                            {
                                obj.category = "";
                            }
                            list.list.Add(obj);
                        }
                    }
                }
                list.message = "fetched";
            }
            catch (Exception ex)
            {
                list.message = ex.Message;
            }
            return list;
        }


    }
}