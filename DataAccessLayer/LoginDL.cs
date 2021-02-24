using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdityaMinerals.EntityModels;
using AdityaMinerals.Models;
using static AdityaMinerals.Models.login;

namespace AdityaMinerals.DataAccessLayer
{
    public class LoginDL
    {
        public CommonOutput CheckLogin(logindata data)
        {
            CommonOutput co = new CommonOutput();
            try
            {
                using(AdityamineralsEntities objDB = new AdityamineralsEntities())
                {
                    var count = objDB.ADM_M_USER.Where(c=>c.UserName==data.UserName && c.Password == data.Password).Count();
                    if(count>0)
                    {
                        co.StatusCode = 200;
                        co.Message = "Welcome! " + data.UserName;
                    }
                    else
                    {
                        co.StatusCode = 201;
                        co.Message = "User Not Found OR Wrong Creds";
                    }
                }
            }
            catch (Exception ex)
            {
                co.StatusCode = 500;
                co.Message = ex.Message;
            }
            return co;
        }
       
    }
}