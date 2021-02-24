using AdityaMinerals.BussinessLayer;
using AdityaMinerals.EntityModels;
using AdityaMinerals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdityaMinerals.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet]
        public ActionResult MainDashboard()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login","Login");
            }
        }
        
        public ActionResult billgenerator()
        {
            if(Session["UserName"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult UOM()
        {
            if (Session["UserName"] != null)
            {
                List<ADM_M_UOM> output = new List<ADM_M_UOM>();
                DashboardBL obj = new DashboardBL();
                output = obj.UOMList();
                return View(output);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult ADDUOM()
        {
            if (Session["UserName"] != null)
            {
               
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public dynamic addedituom(ADM_M_UOM uomdata)
        {
            if (Session["UserName"] != null)
            {
                CommonOutput output = new CommonOutput();
                DashboardBL obj = new DashboardBL();
                output = obj.ADDUOM(uomdata);
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }
    }
}