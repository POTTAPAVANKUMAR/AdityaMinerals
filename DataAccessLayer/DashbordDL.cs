using AdityaMinerals.EntityModels;
using AdityaMinerals.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdityaMinerals.DataAccessLayer
{
    public class DashbordDL
    {
        public List<ADM_M_UOM> UOMListdl()
        {
            List<ADM_M_UOM> output = new List<ADM_M_UOM>();
            try
            {
                using (AdityamineralsEntities objDB = new AdityamineralsEntities())
                {
                    output = objDB.ADM_M_UOM.ToList();
                }
            }
            catch(Exception ex)
            {
                    
            }
            return output;
        }
        public CommonOutput ADDUOM(ADM_M_UOM uomdata)
        {
            CommonOutput output = new CommonOutput();
            try
            {
                using (AdityamineralsEntities objDB = new AdityamineralsEntities())
                {
                    SqlParameter uomid = new SqlParameter("@uomid", uomdata.UOM_ID);
                    SqlParameter uomname = new SqlParameter("@uomname", uomdata.UOMName_VC);
                    SqlParameter uomdesc = new SqlParameter("@uomdesc", uomdata.UOMDesc_VC);
                    SqlParameter singleqty = new SqlParameter("@singleqty", uomdata.SINGLEQTY);
                    output = objDB.Database.SqlQuery<CommonOutput>("[dbo].[ADM_ADDEDITUOM] @uomid,@uomname,@uomdesc,@singleqty", uomid, uomname, uomdesc, singleqty).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                output.StatusCode = 500;
                output.Message = "ERROR";
            }
            return output;
        }
        public CommonOutput addeditbillingprod(billingprodedit data)
        {
            CommonOutput output = new CommonOutput();
            try
            {
                if(data.sno==null || data.sno==0)
                {
                    data.sno = -1;
                }
                using (AdityamineralsEntities objDB = new AdityamineralsEntities())
                {
                    SqlParameter uomid = new SqlParameter("@sno", data.sno);
                    SqlParameter uomname = new SqlParameter("@name", data.productname);
                    SqlParameter uomdesc = new SqlParameter("@uomid", data.uomid);
                    output = objDB.Database.SqlQuery<CommonOutput>("[dbo].[ADM_ADDEDITBILLINGPRODUCTS] @sno,@name,@uomid", uomid, uomname, uomdesc).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                output.StatusCode = 500;
                output.Message = "ERROR";
            }
            return output;
        }
    }
}