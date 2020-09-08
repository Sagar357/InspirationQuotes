using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverydayPower.Models
{
    public class Faq_model
    {
        public string questions { get; set; }
        public string answer { get; set; }

    }

    public class Faq_model_list
    {
        public Faq_model_list()
        {
            this.list = new List<Faq_model>();
        }
        public List<Faq_model> list { get; set; }
        public string message { get; set; }
    }
}