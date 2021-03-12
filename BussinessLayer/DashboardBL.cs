using AdityaMinerals.EntityModels;
using System;
using System.Collections.Generic;
using AdityaMinerals.DataAccessLayer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdityaMinerals.Models;

namespace AdityaMinerals.BussinessLayer
{
    public class DashboardBL
    {
        public List<ADM_M_UOM> UOMList()
        {
            List<ADM_M_UOM> output = new List<ADM_M_UOM>();
            try
            {
               
                DashbordDL obj = new DashbordDL();
                output = obj.UOMListdl();
                
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
                DashbordDL obj = new DashbordDL();
                output = obj.ADDUOM(uomdata);
            }
            catch (Exception ex)
            {

            }
            return output;
        }
        public CommonOutput addeditbillingprod(billingprodedit data)
        {
            CommonOutput output = new CommonOutput();
            try
            {
                DashbordDL obj = new DashbordDL();
                output = obj.addeditbillingprod(data);
            }
            catch (Exception ex)
            {

            }
            return output;
        }
        public List<ADM_M_BILLINGPRODUCTS> getproductslist()
        {
            List<ADM_M_BILLINGPRODUCTS> obj = new List<ADM_M_BILLINGPRODUCTS>();
            try
            {
                DashbordDL obja = new DashbordDL();
                obj = obja.getproductslist();
            }
            catch (Exception ex)
            {
            }
            return obj;
        }
        public CommonOutput savebp2(savebp2model req)
        {
            CommonOutput output = new CommonOutput();
            try
            {
                DashbordDL obj = new DashbordDL();
                
                output = obj.savebp2(req);

            }
            catch(Exception ex)
            {

            }
            return output;
        }
        public CommonOutput savebp1(savebp1model req)
        {
            CommonOutput output = new CommonOutput();
            try
            {
                DashbordDL obj = new DashbordDL();

                output = obj.savebp1(req);

            }
            catch (Exception ex)
            {

            }
            return output;
        }
        public CommonOutput savebp1edit(savebp1model req)
        {
            CommonOutput output = new CommonOutput();
            try
            {
                DashbordDL obj = new DashbordDL();

                output = obj.savebp1edit(req);

            }
            catch (Exception ex)
            {

            }
            return output;
        }
        public CommonOutput deletebill(string invono)
        {
            CommonOutput output = new CommonOutput();
            try
            {
                DashbordDL obj = new DashbordDL();
                output = obj.deletebill(invono);
            }
            catch(Exception ex)
            {

            }
            return output;
        }
        public CommonOutput deletesubbill(string invono,string id)
        {
            CommonOutput output = new CommonOutput();
            try
            {
                DashbordDL obj = new DashbordDL();
                output = obj.deletesubbill(invono,id);
            }
            catch (Exception ex)
            {

            }
            return output;
        }
    }
}