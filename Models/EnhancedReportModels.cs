using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdityaMinerals.EntityModels;

namespace AdityaMinerals.Models
{
    public enum ReportFormat
    {
        PDF,
        Excel,
        CSV,
        JSON
    }

    public enum ReportType
    {
        Invoice,
        ProductSummary,
        SalesReport,
        CustomerReport,
        InventoryReport
    }

    public class ReportRequest
    {
        public ReportType ReportType { get; set; }
        public ReportFormat Format { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? InvoiceId { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }

    public class EnhancedReportModel
    {
        public string ReportTitle { get; set; }
        public DateTime GeneratedDate { get; set; }
        public string GeneratedBy { get; set; }
        public ReportType ReportType { get; set; }
        public object Data { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }

    public class InvoiceReportData
    {
        public ADM_L_BILLINGPART1 InvoiceHeader { get; set; }
        public List<ADM_L_BILLINGPART2> InvoiceItems { get; set; }
        public List<ADM_M_BILLINGPRODUCTS> Products { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetAmount { get; set; }
        public int TotalItems { get; set; }
    }

    public class SalesReportData
    {
        public DateTime ReportPeriodStart { get; set; }
        public DateTime ReportPeriodEnd { get; set; }
        public List<SalesSummaryItem> SalesItems { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalDiscount { get; set; }
        public int TotalInvoices { get; set; }
        public List<TopSellingProduct> TopProducts { get; set; }
    }

    public class SalesSummaryItem
    {
        public int InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CustomerName { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string Status { get; set; }
    }

    public class TopSellingProduct
    {
        public string ProductName { get; set; }
        public string HSNCode { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalValue { get; set; }
        public int OrderCount { get; set; }
    }

    public class ProductReportData
    {
        public List<ProductSummaryItem> Products { get; set; }
        public int TotalProducts { get; set; }
        public decimal TotalInventoryValue { get; set; }
    }

    public class ProductSummaryItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string HSNCode { get; set; }
        public string UOM { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class CustomerReportData
    {
        public List<CustomerSummaryItem> Customers { get; set; }
        public int TotalCustomers { get; set; }
        public decimal TotalBusinessValue { get; set; }
    }

    public class CustomerSummaryItem
    {
        public string CustomerName { get; set; }
        public string GSTIN { get; set; }
        public string State { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalPurchaseValue { get; set; }
        public DateTime LastOrderDate { get; set; }
        public string Status { get; set; }
    }
}
