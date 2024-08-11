﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdityaMinerals.EntityModels
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AdityamineralsEntities : DbContext
    {
        public AdityamineralsEntities()
            : base("name=AdityamineralsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADM_L_BILLINGPART1> ADM_L_BILLINGPART1 { get; set; }
        public virtual DbSet<ADM_L_BILLINGPART2> ADM_L_BILLINGPART2 { get; set; }
        public virtual DbSet<ADM_M_BILLINGPRODUCTS> ADM_M_BILLINGPRODUCTS { get; set; }
        public virtual DbSet<ADM_M_UOM> ADM_M_UOM { get; set; }
        public virtual DbSet<ADM_M_USER> ADM_M_USER { get; set; }
    
        public virtual ObjectResult<ADM_ADDEDITBILLINGPRODUCTS_Result> ADM_ADDEDITBILLINGPRODUCTS(Nullable<int> sNO, string name, Nullable<int> uomID)
        {
            var sNOParameter = sNO.HasValue ?
                new ObjectParameter("SNO", sNO) :
                new ObjectParameter("SNO", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var uomIDParameter = uomID.HasValue ?
                new ObjectParameter("uomID", uomID) :
                new ObjectParameter("uomID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ADM_ADDEDITBILLINGPRODUCTS_Result>("ADM_ADDEDITBILLINGPRODUCTS", sNOParameter, nameParameter, uomIDParameter);
        }
    
        public virtual ObjectResult<ADM_ADDEDITUOM_Result> ADM_ADDEDITUOM(Nullable<int> uomid, string uomname, string uomdesc, Nullable<int> singleqty)
        {
            var uomidParameter = uomid.HasValue ?
                new ObjectParameter("uomid", uomid) :
                new ObjectParameter("uomid", typeof(int));
    
            var uomnameParameter = uomname != null ?
                new ObjectParameter("uomname", uomname) :
                new ObjectParameter("uomname", typeof(string));
    
            var uomdescParameter = uomdesc != null ?
                new ObjectParameter("uomdesc", uomdesc) :
                new ObjectParameter("uomdesc", typeof(string));
    
            var singleqtyParameter = singleqty.HasValue ?
                new ObjectParameter("singleqty", singleqty) :
                new ObjectParameter("singleqty", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ADM_ADDEDITUOM_Result>("ADM_ADDEDITUOM", uomidParameter, uomnameParameter, uomdescParameter, singleqtyParameter);
        }
    
        public virtual ObjectResult<ADM_DELINVOICE_Result> ADM_DELINVOICE(Nullable<int> invoiceid)
        {
            var invoiceidParameter = invoiceid.HasValue ?
                new ObjectParameter("invoiceid", invoiceid) :
                new ObjectParameter("invoiceid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ADM_DELINVOICE_Result>("ADM_DELINVOICE", invoiceidParameter);
        }
    
        public virtual ObjectResult<ADM_DELSUBINVOICE_Result> ADM_DELSUBINVOICE(Nullable<int> invoiceid, Nullable<int> id)
        {
            var invoiceidParameter = invoiceid.HasValue ?
                new ObjectParameter("invoiceid", invoiceid) :
                new ObjectParameter("invoiceid", typeof(int));
    
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ADM_DELSUBINVOICE_Result>("ADM_DELSUBINVOICE", invoiceidParameter, idParameter);
        }
    
        public virtual ObjectResult<ADM_SAVEBP1_Result> ADM_SAVEBP1(string state, Nullable<int> statecode, Nullable<System.DateTime> date, string bpname, string bpaddress, string bpgstin, string bpstate, Nullable<int> bpstatecode, string spname, string spaddress, string spgstin, string spstate, Nullable<int> spstatecode)
        {
            var stateParameter = state != null ?
                new ObjectParameter("state", state) :
                new ObjectParameter("state", typeof(string));
    
            var statecodeParameter = statecode.HasValue ?
                new ObjectParameter("statecode", statecode) :
                new ObjectParameter("statecode", typeof(int));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            var bpnameParameter = bpname != null ?
                new ObjectParameter("bpname", bpname) :
                new ObjectParameter("bpname", typeof(string));
    
            var bpaddressParameter = bpaddress != null ?
                new ObjectParameter("bpaddress", bpaddress) :
                new ObjectParameter("bpaddress", typeof(string));
    
            var bpgstinParameter = bpgstin != null ?
                new ObjectParameter("bpgstin", bpgstin) :
                new ObjectParameter("bpgstin", typeof(string));
    
            var bpstateParameter = bpstate != null ?
                new ObjectParameter("bpstate", bpstate) :
                new ObjectParameter("bpstate", typeof(string));
    
            var bpstatecodeParameter = bpstatecode.HasValue ?
                new ObjectParameter("bpstatecode", bpstatecode) :
                new ObjectParameter("bpstatecode", typeof(int));
    
            var spnameParameter = spname != null ?
                new ObjectParameter("spname", spname) :
                new ObjectParameter("spname", typeof(string));
    
            var spaddressParameter = spaddress != null ?
                new ObjectParameter("spaddress", spaddress) :
                new ObjectParameter("spaddress", typeof(string));
    
            var spgstinParameter = spgstin != null ?
                new ObjectParameter("spgstin", spgstin) :
                new ObjectParameter("spgstin", typeof(string));
    
            var spstateParameter = spstate != null ?
                new ObjectParameter("spstate", spstate) :
                new ObjectParameter("spstate", typeof(string));
    
            var spstatecodeParameter = spstatecode.HasValue ?
                new ObjectParameter("spstatecode", spstatecode) :
                new ObjectParameter("spstatecode", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ADM_SAVEBP1_Result>("ADM_SAVEBP1", stateParameter, statecodeParameter, dateParameter, bpnameParameter, bpaddressParameter, bpgstinParameter, bpstateParameter, bpstatecodeParameter, spnameParameter, spaddressParameter, spgstinParameter, spstateParameter, spstatecodeParameter);
        }
    
        public virtual ObjectResult<ADM_SAVEBP1EDIT_Result> ADM_SAVEBP1EDIT(Nullable<int> invoiceNo, string state, Nullable<int> statecode, Nullable<System.DateTime> date, string bpname, string bpaddress, string bpgstin, string bpstate, Nullable<int> bpstatecode, string spname, string spaddress, string spgstin, string spstate, Nullable<int> spstatecode)
        {
            var invoiceNoParameter = invoiceNo.HasValue ?
                new ObjectParameter("invoiceNo", invoiceNo) :
                new ObjectParameter("invoiceNo", typeof(int));
    
            var stateParameter = state != null ?
                new ObjectParameter("state", state) :
                new ObjectParameter("state", typeof(string));
    
            var statecodeParameter = statecode.HasValue ?
                new ObjectParameter("statecode", statecode) :
                new ObjectParameter("statecode", typeof(int));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            var bpnameParameter = bpname != null ?
                new ObjectParameter("bpname", bpname) :
                new ObjectParameter("bpname", typeof(string));
    
            var bpaddressParameter = bpaddress != null ?
                new ObjectParameter("bpaddress", bpaddress) :
                new ObjectParameter("bpaddress", typeof(string));
    
            var bpgstinParameter = bpgstin != null ?
                new ObjectParameter("bpgstin", bpgstin) :
                new ObjectParameter("bpgstin", typeof(string));
    
            var bpstateParameter = bpstate != null ?
                new ObjectParameter("bpstate", bpstate) :
                new ObjectParameter("bpstate", typeof(string));
    
            var bpstatecodeParameter = bpstatecode.HasValue ?
                new ObjectParameter("bpstatecode", bpstatecode) :
                new ObjectParameter("bpstatecode", typeof(int));
    
            var spnameParameter = spname != null ?
                new ObjectParameter("spname", spname) :
                new ObjectParameter("spname", typeof(string));
    
            var spaddressParameter = spaddress != null ?
                new ObjectParameter("spaddress", spaddress) :
                new ObjectParameter("spaddress", typeof(string));
    
            var spgstinParameter = spgstin != null ?
                new ObjectParameter("spgstin", spgstin) :
                new ObjectParameter("spgstin", typeof(string));
    
            var spstateParameter = spstate != null ?
                new ObjectParameter("spstate", spstate) :
                new ObjectParameter("spstate", typeof(string));
    
            var spstatecodeParameter = spstatecode.HasValue ?
                new ObjectParameter("spstatecode", spstatecode) :
                new ObjectParameter("spstatecode", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ADM_SAVEBP1EDIT_Result>("ADM_SAVEBP1EDIT", invoiceNoParameter, stateParameter, statecodeParameter, dateParameter, bpnameParameter, bpaddressParameter, bpgstinParameter, bpstateParameter, bpstatecodeParameter, spnameParameter, spaddressParameter, spgstinParameter, spstateParameter, spstatecodeParameter);
        }
    
        public virtual ObjectResult<ADM_SAVEBP2_Result> ADM_SAVEBP2(Nullable<int> invoiceNo, string proddesc, string hsncode, string uom, Nullable<int> qty, Nullable<int> rate, Nullable<int> amount, Nullable<int> discount, Nullable<int> valueofsupply, Nullable<int> prodid)
        {
            var invoiceNoParameter = invoiceNo.HasValue ?
                new ObjectParameter("InvoiceNo", invoiceNo) :
                new ObjectParameter("InvoiceNo", typeof(int));
    
            var proddescParameter = proddesc != null ?
                new ObjectParameter("proddesc", proddesc) :
                new ObjectParameter("proddesc", typeof(string));
    
            var hsncodeParameter = hsncode != null ?
                new ObjectParameter("hsncode", hsncode) :
                new ObjectParameter("hsncode", typeof(string));
    
            var uomParameter = uom != null ?
                new ObjectParameter("uom", uom) :
                new ObjectParameter("uom", typeof(string));
    
            var qtyParameter = qty.HasValue ?
                new ObjectParameter("qty", qty) :
                new ObjectParameter("qty", typeof(int));
    
            var rateParameter = rate.HasValue ?
                new ObjectParameter("rate", rate) :
                new ObjectParameter("rate", typeof(int));
    
            var amountParameter = amount.HasValue ?
                new ObjectParameter("amount", amount) :
                new ObjectParameter("amount", typeof(int));
    
            var discountParameter = discount.HasValue ?
                new ObjectParameter("discount", discount) :
                new ObjectParameter("discount", typeof(int));
    
            var valueofsupplyParameter = valueofsupply.HasValue ?
                new ObjectParameter("valueofsupply", valueofsupply) :
                new ObjectParameter("valueofsupply", typeof(int));
    
            var prodidParameter = prodid.HasValue ?
                new ObjectParameter("prodid", prodid) :
                new ObjectParameter("prodid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ADM_SAVEBP2_Result>("ADM_SAVEBP2", invoiceNoParameter, proddescParameter, hsncodeParameter, uomParameter, qtyParameter, rateParameter, amountParameter, discountParameter, valueofsupplyParameter, prodidParameter);
        }
    }
}
