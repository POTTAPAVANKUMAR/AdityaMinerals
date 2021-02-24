using AdityaMinerals.EntityModels;
using AdityaMinerals.Models;
using System;
using System.Collections.Generic;
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
                    if (uomdata.UOM_ID == -1)
                    {
                        var insertobj = new ADM_M_UOM
                        {
                            UOMName_VC = uomdata.UOMName_VC,
                            UOMDesc_VC=uomdata.UOMDesc_VC,
                            SINGLEQTY=uomdata.SINGLEQTY
                        };
                        objDB.ADM_M_UOM.Add(insertobj);
                        objDB.SaveChanges();
                        output.StatusCode = 200;
                        output.Message = "Added Successfully";
                    }
                    else
                    {
                        var updateobj = objDB.ADM_M_UOM.Where(c => c.UOM_ID == uomdata.UOM_ID).FirstOrDefault();
                        if (uomdata.UOMName_VC != null)
                        {
                            updateobj.UOMName_VC = uomdata.UOMName_VC;
                        }
                        if(uomdata.UOMDesc_VC!=null)
                        {
                            updateobj.UOMDesc_VC = uomdata.UOMDesc_VC;
                        }
                        updateobj.SINGLEQTY = uomdata.SINGLEQTY;
                        objDB.SaveChanges();
                        output.StatusCode = 200;
                        output.Message = "Updated Successfully";
                    }
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