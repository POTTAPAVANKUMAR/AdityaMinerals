using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdityaMinerals.EntityModels;
using AdityaMinerals.Models;
using System.Web.Mvc;

namespace AdityaMinerals.DataAccessLayer
{
    public class PDFDLController 
    {
        public dynamic Reportpdf(int Invoiceno)
        {
            reportmodel output = new reportmodel();
            try
            {
                
                using(AdityamineralsEntities objDB = new AdityamineralsEntities())
                {
                    output.bp = objDB.ADM_M_BILLINGPRODUCTS.ToList();
                    output.bp1 = objDB.ADM_L_BILLINGPART1.Where(c => c.InvoiceNo == Invoiceno).FirstOrDefault();
                    output.bp2 = objDB.ADM_L_BILLINGPART2.Where(c => c.InvoiceNo == Invoiceno).ToList();
                }
                return output;
            }
            catch(Exception ex)
            {
                return output;
            }
        }
    }
}