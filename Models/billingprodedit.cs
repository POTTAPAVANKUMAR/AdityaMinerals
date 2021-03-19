using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdityaMinerals.EntityModels;
namespace AdityaMinerals.Models
{
    public class billingprodedit 
    {
        public int sno { get; set; }
        public string productname { get; set; }
        public int uomid { get; set; }
    }
    public class req
    {
        public int prodid { get; set; }
        public long invoiceno { get; set; }
        public string proddesc { get; set; }
        public string uom { get; set; }
        public ADM_L_BILLINGPART2 bp2 {get;set;}
       
    }
    public class savebp2model
    {
        public long invoiceno { get; set; }
        public int prodid { get; set; }
        public string hsncode { get; set; }
        public string uom { get; set; }
        public int qty { get; set; }
        public int rate { get; set; }
        public int amount { get; set; }
        public int discount { get; set; }
        public int valueofsupply { get; set; }
      
    }
    public class savebp1model
    {
        public string state { get; set; }
        public int statecode { get; set; }
        public string DATE1 { get; set; }
        public DateTime date { get; set; }
        public string bpname { get; set; }
        public string bpaddress { get; set; }
        public string bpgstin { get; set; }
        public string bpstate { get; set; }
        public int bpstatecode { get; set; }
        public string spname { get; set; }
        public string spaddress { get; set; }
        public string spgstin { get; set; }
        public string spstate { get; set; }
        public int spstatecode { get; set; }
    }
    public class deletebill
    {
        public string invoiceno { get; set; }
        public string id { get; set; }
    }
    public class billeditmain
    {
        public ADM_L_BILLINGPART1 BP1 { get; set; }
        public List<ADM_L_BILLINGPART2> BP2 { get; set; }
        public List<ADM_M_BILLINGPRODUCTS> BP3 { get; set; }
    }
    public class graph1
    {
        public int QTY { get; set; }
        public string PD { get; set; }
    }
    public class op
    {
        public List<graph1> output { get; set; }
        public string output1 { get; set; }
    }
}