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
        public ActionResult billingproducts()
        {
            if (Session["UserName"] != null)
            {
                List<ADM_M_BILLINGPRODUCTS> output = new List<ADM_M_BILLINGPRODUCTS>();
                using(AdityamineralsEntities objDB =new AdityamineralsEntities())
                {
                    output = objDB.ADM_M_BILLINGPRODUCTS.ToList();
                }
                return View(output);
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
        public dynamic addedituom(ADM_M_UOM uomdata)
        {
            if (Session["UserName"] != null)
            {
                CommonOutput output = new CommonOutput();
                DashboardBL obj = new DashboardBL();
                output = obj.ADDUOM(uomdata);
                return Json(output,JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }
        public dynamic addeditbillingprod(billingprodedit data)
        {
            if (Session["UserName"] != null)
            {
                CommonOutput output = new CommonOutput();
                DashboardBL obj = new DashboardBL();
                output = obj.addeditbillingprod(data);
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public dynamic uomlist()
        {
            AdityamineralsEntities objDB = new AdityamineralsEntities();
            var list = objDB.ADM_M_UOM.ToList();
            var jsonresult = list.Select(x => new
            {
                id=x.UOM_ID,
                text = x.UOMName_VC + "-(" + x.UOMDesc_VC + ")"
            });
            return Json(jsonresult, JsonRequestBehavior.AllowGet);
        }
      
    }
}