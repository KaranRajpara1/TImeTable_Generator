using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeTale_Generator.Controllers
{
    public class BaseController : Controller
    {
        
        public bool IsValidUser()
        {
            if (Session["ID"] != null)
            {
                return true;
            }
            else return false;
        }
    }
}