using System;
using System.Collections.Generic;
using System.Linq;
using AdityaMinerals.DataAccessLayer;
using AdityaMinerals.EntityModels;
using AdityaMinerals.Models;

namespace AdityaMinerals.BussinessLayer
{
    public class EnhancedReportBL
    {
        private readonly EnhancedReportDL _reportDL;

        public EnhancedReportBL()
        {
            _reportDL = new EnhancedReportDL();
        }

        public EnhancedReportModel GenerateReport(ReportRequest request, string userName)
        {
            try
            {
                var report = new EnhancedReportModel
                {
                    GeneratedDate = DateTime.Now,
                    GeneratedBy = userName,
                    ReportType = request.ReportType
                };

                switch (request.ReportType)
                {
                    case ReportType.Invoice:
                        report.Data = GenerateInvoiceReport(request.InvoiceId.Value);
                        report.ReportTitle = $"Invoice Report - {request.InvoiceId}";
                        break;

                    case ReportType.SalesReport:
                        report.Data = GenerateSalesReport(request.StartDate, request.EndDate);
                        report.ReportTitle = $"Sales Report - {request.StartDate?.ToString("dd/MM/yyyy")} to {request.EndDate?.ToString("dd/MM/yyyy")}";
                        break;

                    case ReportType.ProductSummary:
                        report.Data = GenerateProductReport();
                        report.ReportTitle = "Product Summary Report";
                        break;

                    case ReportType.CustomerReport:
                        report.Data = GenerateCustomerReport(request.StartDate, request.EndDate);
                        report.ReportTitle = "Customer Report";
                        break;

                    case ReportType.InventoryReport:
                        report.Data = GenerateInventoryReport();
                        report.ReportTitle = "Inventory Report";
                        break;

                    default:
                        throw new ArgumentException("Invalid report type");
                }

                // Add metadata
                report.Metadata.Add("TotalRecords", GetRecordCount(report.Data));
                report.Metadata.Add("GenerationTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                report.Metadata.Add("RequestParameters", request);

                return report;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating report: {ex.Message}", ex);
            }
        }

        private InvoiceReportData GenerateInvoiceReport(int invoiceId)
        {
            var invoiceData = _reportDL.GetInvoiceData(invoiceId);
            
            if (invoiceData.InvoiceHeader == null)
                throw new ArgumentException($"Invoice {invoiceId} not found");

            var reportData = new InvoiceReportData
            {
                InvoiceHeader = invoiceData.InvoiceHeader,
                InvoiceItems = invoiceData.InvoiceItems,
                Products = invoiceData.Products,
                TotalItems = invoiceData.InvoiceItems?.Count ?? 0
            };

            // Calculate totals
            if (invoiceData.InvoiceItems != null)
            {
                reportData.TotalAmount = invoiceData.InvoiceItems.Sum(x => x.Amount ?? 0);
                reportData.TotalDiscount = invoiceData.InvoiceItems.Sum(x => x.Discount ?? 0);
                reportData.NetAmount = invoiceData.InvoiceItems.Sum(x => x.ValueofSupply ?? 0);
            }

            return reportData;
        }

        private SalesReportData GenerateSalesReport(DateTime? startDate, DateTime? endDate)
        {
            var salesData = _reportDL.GetSalesData(startDate, endDate);
            
            var reportData = new SalesReportData
            {
                ReportPeriodStart = startDate ?? DateTime.MinValue,
                ReportPeriodEnd = endDate ?? DateTime.MaxValue,
                SalesItems = new List<SalesSummaryItem>(),
                TopProducts = new List<TopSellingProduct>()
            };

            // Process sales data
            foreach (var invoice in salesData.Invoices)
            {
                var salesItem = new SalesSummaryItem
                {
                    InvoiceNo = invoice.InvoiceNo ?? 0,
                    InvoiceDate = invoice.DateOfIssue ?? DateTime.MinValue,
                    CustomerName = invoice.BP_Name_VC ?? "Unknown",
                    InvoiceAmount = salesData.InvoiceItems
                        .Where(x => x.InvoiceNo == invoice.InvoiceNo)
                        .Sum(x => x.Amount ?? 0),
                    DiscountAmount = salesData.InvoiceItems
                        .Where(x => x.InvoiceNo == invoice.InvoiceNo)
                        .Sum(x => x.Discount ?? 0),
                    NetAmount = salesData.InvoiceItems
                        .Where(x => x.InvoiceNo == invoice.InvoiceNo)
                        .Sum(x => x.ValueofSupply ?? 0),
                    Status = "Completed"
                };

                reportData.SalesItems.Add(salesItem);
            }

            // Calculate totals
            reportData.TotalSales = reportData.SalesItems.Sum(x => x.InvoiceAmount);
            reportData.TotalDiscount = reportData.SalesItems.Sum(x => x.DiscountAmount);
            reportData.TotalInvoices = reportData.SalesItems.Count;

            // Generate top products
            var productGroups = salesData.InvoiceItems
                .GroupBy(x => x.ProductionDescription_VC)
                .Select(g => new TopSellingProduct
                {
                    ProductName = g.Key ?? "Unknown",
                    HSNCode = g.First().HSNCODE_VC ?? "",
                    TotalQuantity = g.Sum(x => x.QTY ?? 0),
                    TotalValue = g.Sum(x => x.ValueofSupply ?? 0),
                    OrderCount = g.Count()
                })
                .OrderByDescending(x => x.TotalValue)
                .Take(10)
                .ToList();

            reportData.TopProducts = productGroups;

            return reportData;
        }

        private ProductReportData GenerateProductReport()
        {
            var products = _reportDL.GetAllProducts();
            
            var reportData = new ProductReportData
            {
                Products = new List<ProductSummaryItem>(),
                TotalProducts = products.Count
            };

            foreach (var product in products)
            {
                var productItem = new ProductSummaryItem
                {
                    ProductId = product.ProductId ?? 0,
                    ProductName = product.ProductDescription ?? "Unknown",
                    HSNCode = product.HSNCode ?? "",
                    UOM = product.UOM ?? "",
                    CurrentStock = 0, // This would need to be calculated from inventory
                    UnitPrice = 0, // This would need to be fetched from pricing
                    TotalValue = 0,
                    LastUpdated = DateTime.Now
                };

                reportData.Products.Add(productItem);
            }

            reportData.TotalInventoryValue = reportData.Products.Sum(x => x.TotalValue);

            return reportData;
        }

        private CustomerReportData GenerateCustomerReport(DateTime? startDate, DateTime? endDate)
        {
            var customerData = _reportDL.GetCustomerData(startDate, endDate);
            
            var reportData = new CustomerReportData
            {
                Customers = new List<CustomerSummaryItem>()
            };

            var customerGroups = customerData.Invoices
                .GroupBy(x => new { x.BP_Name_VC, x.BP_Gstin_VC, x.BP_State_VC })
                .Select(g => new CustomerSummaryItem
                {
                    CustomerName = g.Key.BP_Name_VC ?? "Unknown",
                    GSTIN = g.Key.BP_Gstin_VC ?? "",
                    State = g.Key.BP_State_VC ?? "",
                    TotalOrders = g.Count(),
                    TotalPurchaseValue = customerData.InvoiceItems
                        .Where(x => g.Any(inv => inv.InvoiceNo == x.InvoiceNo))
                        .Sum(x => x.ValueofSupply ?? 0),
                    LastOrderDate = g.Max(x => x.DateOfIssue ?? DateTime.MinValue),
                    Status = "Active"
                })
                .OrderByDescending(x => x.TotalPurchaseValue)
                .ToList();

            reportData.Customers = customerGroups;
            reportData.TotalCustomers = customerGroups.Count;
            reportData.TotalBusinessValue = customerGroups.Sum(x => x.TotalPurchaseValue);

            return reportData;
        }

        private ProductReportData GenerateInventoryReport()
        {
            // For now, this is similar to product report
            // In a real scenario, this would include stock levels, reorder points, etc.
            return GenerateProductReport();
        }

        private int GetRecordCount(object data)
        {
            switch (data)
            {
                case InvoiceReportData invoice:
                    return invoice.InvoiceItems?.Count ?? 0;
                case SalesReportData sales:
                    return sales.SalesItems?.Count ?? 0;
                case ProductReportData products:
                    return products.Products?.Count ?? 0;
                case CustomerReportData customers:
                    return customers.Customers?.Count ?? 0;
                default:
                    return 0;
            }
        }

        public List<ReportType> GetAvailableReportTypes()
        {
            return Enum.GetValues(typeof(ReportType)).Cast<ReportType>().ToList();
        }

        public List<ReportFormat> GetSupportedFormats()
        {
            return Enum.GetValues(typeof(ReportFormat)).Cast<ReportFormat>().ToList();
        }
    }
}
