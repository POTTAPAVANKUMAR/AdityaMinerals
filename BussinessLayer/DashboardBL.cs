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
    }
}