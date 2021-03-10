using AdityaMinerals.BussinessLayer;
using AdityaMinerals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AdityaMinerals.Models.login;

namespace AdityaMinerals.Controllers
{
    public class LoginController : Controller
    {
        public dynamic Login()
        {
            if (Session["UserName"] == null)
            {
                return View();
            }
            return RedirectToAction("MainDashboard", "Dashboard");
        }
        [HttpPost]
        public dynamic CheckLogin(logindata data)
        {
            CommonOutput output = new CommonOutput();
            try
            {
                LoginBL obj = new LoginBL();
                output = obj.CheckLogin(data);
                if (output.StatusCode == 200)
                {
                    Session["UserName"] = data.UserName.ToString();
                    Session["isAuthenticated"] = '1';
                    
                }
            }
            catch(Exception ex)
            {
                output.Message = ex.Message;
                output.StatusCode = 500;
            }
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        public dynamic logout()
        {
           
            return RedirectToAction("GOOGLE.COM");
           
        }
    }
}