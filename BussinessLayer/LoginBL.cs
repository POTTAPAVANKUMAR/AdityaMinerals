using AdityaMinerals.DataAccessLayer;
using AdityaMinerals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AdityaMinerals.Models.login;

namespace AdityaMinerals.BussinessLayer
{
    public class LoginBL 
    {
        public CommonOutput CheckLogin(logindata data)
        {
            CommonOutput output = new CommonOutput();
            try
            {
                LoginDL obj = new LoginDL();
                output = obj.CheckLogin(data);
            }
            catch(Exception ex)
            {
                output.StatusCode = 500;
                output.Message = ex.Message;
            }
            return output;
        }
    }
}