using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdityaMinerals.EntityModels;

namespace AdityaMinerals.Models
{
    
       public class reportmodel
        {
            public ADM_L_BILLINGPART1 bp1 { get; set; }
            public List<ADM_L_BILLINGPART2> bp2 { get; set; }
            public List<ADM_M_BILLINGPRODUCTS> bp { get; set; }
        }
  
}