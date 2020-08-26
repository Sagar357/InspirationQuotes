using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverydayPower.Models
{
    public class FileSave_Model
    {
        public FileSave_Model(FileSave_Model m)
        {
            this.fileAttachmentCode = m.fileAttachmentCode;
            this.imagePath = m.imagePath;
            this.message = m.message;
            this.imageName = m.imageName;

        }
        public FileSave_Model()
        {
            
        }
        public string imagePath { get; set; }
        public string fileAttachmentCode { get; set; }
        public string imageName { get; set; }
        public string message { get; set; }
    }
}