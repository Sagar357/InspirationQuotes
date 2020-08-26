using EverydayPower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace EverydayPower.Services
{
    public class File_Service
    {

        public FileSave_Model FileSave(FileSave_Model model)
        {
            if (model.message == "success")
            {
                Guid guid = Guid.NewGuid();
                model.fileAttachmentCode = guid.ToString();
                try
                {
                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString()))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "tmp_prc_saveImageFile";
                            cmd.Parameters.AddWithValue("@file_attachment_code", model.fileAttachmentCode);
                            cmd.Parameters.AddWithValue("@file_name", model.imageName);
                            cmd.Parameters.AddWithValue("@file_path", model.imagePath);

                        cmd.ExecuteNonQuery();
                        }
                    model.message = "saved";

                }
                catch (Exception ex)
                {
                    model.message = ex.Message;
                } 
            }
            return model;
        }
    }
}