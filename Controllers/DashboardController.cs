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
            List<ADM_M_BILLINGPRODUCTS> output1 = new List<ADM_M_BILLINGPRODUCTS>();
            if (Session["UserName"]!=null)
            {
                AdityamineralsEntities dbObj = new AdityamineralsEntities();
                var output = dbObj.ADM_L_BILLINGPART1.Count();
                if(output==0)
                {
                    DashboardBL obj = new DashboardBL();
                    output1 = obj.getproductslist();
                    ViewBag.invoiceno = 1;
                }
                else
                {
                    var op = dbObj.ADM_L_BILLINGPART1.Select(c=>c.InvoiceNo).DefaultIfEmpty(0).Max();
                    ViewBag.invoiceno = op + 1;
                    DashboardBL obj = new DashboardBL();
                    output1 = obj.getproductslist();
                }
                return View(output1);
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

        public ActionResult savebp1(req req)
        {
            if (Session["UserName"] != null)
            {
                using(AdityamineralsEntities objDB = new AdityamineralsEntities())
                {
                    req.uom = objDB.ADM_M_BILLINGPRODUCTS.Where(v => v.Sno == req.prodid).Select(c => c.UOM_Name).FirstOrDefault();
                }
                return PartialView("_editbillprod",req);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
           
        }
        public dynamic savebp2(savebp2model req)
        {
            CommonOutput output = new CommonOutput();
            if (Session["UserName"] != null)
            {
                DashboardBL bl = new DashboardBL();
                output = bl.savebp2(req);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        public dynamic savebp11(savebp1model req)
        {
            CommonOutput output = new CommonOutput();
            if (Session["UserName"] != null)
            {
                DashboardBL bl = new DashboardBL();
                output = bl.savebp1(req);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult billslist()
        {
            if (Session["UserName"] != null)
            {
                List<ADM_L_BILLINGPART1> obj = new List<ADM_L_BILLINGPART1>();
                using(AdityamineralsEntities objDB = new AdityamineralsEntities())
                {
                    obj = objDB.ADM_L_BILLINGPART1.ToList();
                }
                ViewBag.data = obj;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

    }
}