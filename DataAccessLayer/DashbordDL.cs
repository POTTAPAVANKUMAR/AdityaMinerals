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

        public List<ADM_M_BILLINGPRODUCTS> getproductslist()
        {
            List<ADM_M_BILLINGPRODUCTS> obj = new List<ADM_M_BILLINGPRODUCTS>();
            try
            {
                using(AdityamineralsEntities objDB =new AdityamineralsEntities())
                {
                    obj = objDB.ADM_M_BILLINGPRODUCTS.ToList();
                }
            }
            catch(Exception ex)
            { 
            }
            return obj;
        }
        public CommonOutput savebp2(savebp2model req)
        {
            CommonOutput output = new CommonOutput();
            try
            {
                using(var objDB = new AdityamineralsEntities())
                {
                    var proddesc = objDB.ADM_M_BILLINGPRODUCTS.Where(c => c.Sno == req.prodid).FirstOrDefault();
                    SqlParameter InvoiceNo = new SqlParameter("@InvoiceNo", Convert.ToInt32(req.invoiceno));
                    SqlParameter ProductionDescription_VC = new SqlParameter("@proddesc", proddesc.ProductDescription);
                    SqlParameter HSNCODE_VC = new SqlParameter("@hsncode", req.hsncode);
                    SqlParameter UOM_VC = new SqlParameter("@uom", req.uom);
                    SqlParameter QTY = new SqlParameter("@qty", req.qty);
                    SqlParameter Rate = new SqlParameter("@rate", req.rate);
                    SqlParameter Amount = new SqlParameter("@amount", req.amount);
                    SqlParameter Discount = new SqlParameter("@discount", req.discount);
                    SqlParameter ValueofSupply = new SqlParameter("@valueofsupply", req.valueofsupply);
                    SqlParameter Sno = new SqlParameter("@prodid", req.prodid);
                    output = objDB.Database.SqlQuery<CommonOutput>("[dbo].[ADM_SAVEBP2] @InvoiceNo,@proddesc,@hsncode,@uom,@qty,@rate,@amount,@discount,@valueofsupply,@prodid",
                    InvoiceNo, ProductionDescription_VC, HSNCODE_VC, UOM_VC, QTY, Rate, Amount, Discount, ValueofSupply, Sno).FirstOrDefault();

                }
            }
            catch(Exception ex)
            {
                output.StatusCode = 500;
                output.Message = ex.Message;
            }
            return output;
        }
        public CommonOutput savebp1(savebp1model req)
        {
            CommonOutput output = new CommonOutput();
            try
            {
                using (var objDB = new AdityamineralsEntities())
                {
                    SqlParameter state = new SqlParameter("@state", req.state);
                    SqlParameter statecode = new SqlParameter("@statecode", req.statecode);
                    SqlParameter date = new SqlParameter("@date", req.date);
                    SqlParameter bpname = new SqlParameter("@bpname", req.bpname);
                    SqlParameter bpaddress = new SqlParameter("@bpaddress", req.bpaddress);
                    SqlParameter bpgstin = new SqlParameter("@bpgstin", req.bpgstin);
                    SqlParameter bpstate = new SqlParameter("@bpstate", req.bpstate);
                    SqlParameter bpstatecode = new SqlParameter("@bpstatecode", req.bpstatecode);
                    SqlParameter spname = new SqlParameter("@spname", req.spname);
                    SqlParameter spaddress = new SqlParameter("@spaddress", req.spaddress);
                    SqlParameter spgstin = new SqlParameter("@spgstin", req.spgstin);
                    SqlParameter spstate = new SqlParameter("@spstate", req.spstate);
                    SqlParameter spstatecode = new SqlParameter("@spstatecode", req.spstatecode);
                    output = objDB.Database.SqlQuery<CommonOutput>("[dbo].[ADM_SAVEBP1] @state,@statecode,@date,@bpname,@bpaddress,@bpgstin,@bpstate,@bpstatecode,@spname,@spaddress,@spgstin,@spstate,@spstatecode",
                    state, statecode, date, bpname, bpaddress, bpgstin, bpstate, bpstatecode, spname, spaddress, spgstin, spstate, spstatecode).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                output.StatusCode = 500;
                output.Message = ex.Message;
            }
            return output;
        }
    }
    
   
}
