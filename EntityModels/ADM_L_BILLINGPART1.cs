//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class ADM_L_BILLINGPART1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADM_L_BILLINGPART1()
        {
            this.ADM_L_BILLINGPART2 = new HashSet<ADM_L_BILLINGPART2>();
        }
    
        public int InvoiceNo { get; set; }
        public string State_VC { get; set; }
        public Nullable<int> StateCode { get; set; }
        public Nullable<System.DateTime> DateOfIssue { get; set; }
        public string BP_Name_VC { get; set; }
        public string BP_Address_VC { get; set; }
        public string BP_Gstin_VC { get; set; }
        public string BP_State_VC { get; set; }
        public string BP_StateCode_VC { get; set; }
        public string SP_Name_VC1 { get; set; }
        public string SP_Address_VC1 { get; set; }
        public string SP_Gstin_VC1 { get; set; }
        public string SP_State_VC1 { get; set; }
        public string SP_StateCode_VC1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADM_L_BILLINGPART2> ADM_L_BILLINGPART2 { get; set; }
    }
}
