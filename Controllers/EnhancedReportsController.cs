using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AdityaMinerals.BussinessLayer;
using AdityaMinerals.Models;

namespace AdityaMinerals.Controllers
{
    public class EnhancedReportsController : Controller
    {
        private readonly EnhancedReportBL _reportBL;
        private readonly ReportGenerationService _reportService;

        public EnhancedReportsController()
        {
            _reportBL = new EnhancedReportBL();
            _reportService = new ReportGenerationService();
        }

        // GET: EnhancedReports
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.ReportTypes = _reportBL.GetAvailableReportTypes();
            ViewBag.ReportFormats = _reportBL.GetSupportedFormats();
            
            return View();
        }

        // GET: Report Builder
        public ActionResult ReportBuilder()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var model = new ReportBuilderViewModel
            {
                AvailableReportTypes = _reportBL.GetAvailableReportTypes(),
                SupportedFormats = _reportBL.GetSupportedFormats(),
                Request = new ReportRequest
                {
                    StartDate = DateTime.Now.AddMonths(-1),
                    EndDate = DateTime.Now,
                    Format = ReportFormat.PDF,
                    ReportType = ReportType.SalesReport
                }
            };

            return View(model);
        }

        // POST: Generate Report
        [HttpPost]
        public ActionResult GenerateReport(ReportRequest request)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return Json(new { success = false, message = "Session expired. Please login again." });
                }

                var userName = Session["UserName"].ToString();

                // Validate request
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Invalid request parameters." });
                }

                // Generate the report data
                var reportModel = _reportBL.GenerateReport(request, userName);

                // Generate the report file
                var reportBytes = _reportService.GenerateReport(reportModel, request.Format);

                // Create filename
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var filename = $"{request.ReportType}_{timestamp}{_reportService.GetFileExtension(request.Format)}";

                // Return file
                return File(reportBytes, _reportService.GetContentType(request.Format), filename);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error generating report: {ex.Message}" });
            }
        }

        // GET: Preview Report Data (AJAX)
        [HttpPost]
        public JsonResult PreviewReportData(ReportRequest request)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return Json(new { success = false, message = "Session expired" });
                }

                var userName = Session["UserName"].ToString();
                var reportModel = _reportBL.GenerateReport(request, userName);

                // Return preview data based on report type
                object previewData = null;
                switch (request.ReportType)
                {
                    case ReportType.SalesReport:
                        var salesData = (SalesReportData)reportModel.Data;
                        previewData = new
                        {
                            TotalSales = salesData.TotalSales,
                            TotalInvoices = salesData.TotalInvoices,
                            TotalDiscount = salesData.TotalDiscount,
                            SampleItems = salesData.SalesItems?.Take(5).Select(x => new
                            {
                                x.InvoiceNo,
                                InvoiceDate = x.InvoiceDate.ToString("yyyy-MM-dd"),
                                x.CustomerName,
                                x.InvoiceAmount,
                                x.NetAmount
                            })
                        };
                        break;

                    case ReportType.ProductSummary:
                        var productData = (ProductReportData)reportModel.Data;
                        previewData = new
                        {
                            TotalProducts = productData.TotalProducts,
                            TotalInventoryValue = productData.TotalInventoryValue,
                            SampleProducts = productData.Products?.Take(5).Select(x => new
                            {
                                x.ProductName,
                                x.HSNCode,
                                x.UOM,
                                x.UnitPrice
                            })
                        };
                        break;

                    case ReportType.CustomerReport:
                        var customerData = (CustomerReportData)reportModel.Data;
                        previewData = new
                        {
                            TotalCustomers = customerData.TotalCustomers,
                            TotalBusinessValue = customerData.TotalBusinessValue,
                            SampleCustomers = customerData.Customers?.Take(5).Select(x => new
                            {
                                x.CustomerName,
                                x.GSTIN,
                                x.State,
                                x.TotalOrders,
                                x.TotalPurchaseValue
                            })
                        };
                        break;

                    case ReportType.Invoice:
                        var invoiceData = (InvoiceReportData)reportModel.Data;
                        previewData = new
                        {
                            InvoiceNo = invoiceData.InvoiceHeader?.InvoiceNo,
                            InvoiceDate = invoiceData.InvoiceHeader?.DateOfIssue?.ToString("yyyy-MM-dd"),
                            CustomerName = invoiceData.InvoiceHeader?.BP_Name_VC,
                            TotalItems = invoiceData.TotalItems,
                            TotalAmount = invoiceData.TotalAmount,
                            NetAmount = invoiceData.NetAmount
                        };
                        break;

                    default:
                        previewData = new { Message = "Preview not available for this report type" };
                        break;
                }

                return Json(new
                {
                    success = true,
                    reportTitle = reportModel.ReportTitle,
                    generatedDate = reportModel.GeneratedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    totalRecords = reportModel.Metadata.ContainsKey("TotalRecords") ? reportModel.Metadata["TotalRecords"] : 0,
                    data = previewData
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: Report Templates
        public ActionResult Templates()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var templates = GetPredefinedTemplates();
            return View(templates);
        }

        // GET: Generate from Template
        public ActionResult GenerateFromTemplate(string templateId)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var template = GetPredefinedTemplates().FirstOrDefault(t => t.Id == templateId);
            if (template == null)
            {
                return HttpNotFound("Template not found");
            }

            var model = new ReportBuilderViewModel
            {
                AvailableReportTypes = _reportBL.GetAvailableReportTypes(),
                SupportedFormats = _reportBL.GetSupportedFormats(),
                Request = template.DefaultRequest,
                SelectedTemplate = template
            };

            return View("ReportBuilder", model);
        }

        // GET: Report History (placeholder for future implementation)
        public ActionResult History()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            // This would typically load from a database table storing report generation history
            var history = new List<ReportHistoryItem>
            {
                new ReportHistoryItem
                {
                    Id = 1,
                    ReportType = ReportType.SalesReport,
                    Format = ReportFormat.PDF,
                    GeneratedDate = DateTime.Now.AddDays(-1),
                    GeneratedBy = Session["UserName"].ToString(),
                    Status = "Completed",
                    FileName = "SalesReport_20241027.pdf"
                }
            };

            return View(history);
        }

        private List<ReportTemplate> GetPredefinedTemplates()
        {
            return new List<ReportTemplate>
            {
                new ReportTemplate
                {
                    Id = "monthly-sales",
                    Name = "Monthly Sales Report",
                    Description = "Comprehensive sales report for the current month",
                    Category = "Sales",
                    DefaultRequest = new ReportRequest
                    {
                        ReportType = ReportType.SalesReport,
                        Format = ReportFormat.PDF,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                        EndDate = DateTime.Now
                    }
                },
                new ReportTemplate
                {
                    Id = "product-inventory",
                    Name = "Product Inventory Report",
                    Description = "Current product inventory and stock levels",
                    Category = "Inventory",
                    DefaultRequest = new ReportRequest
                    {
                        ReportType = ReportType.ProductSummary,
                        Format = ReportFormat.Excel
                    }
                },
                new ReportTemplate
                {
                    Id = "customer-analysis",
                    Name = "Customer Analysis Report",
                    Description = "Customer purchase patterns and analysis",
                    Category = "Customer",
                    DefaultRequest = new ReportRequest
                    {
                        ReportType = ReportType.CustomerReport,
                        Format = ReportFormat.PDF,
                        StartDate = DateTime.Now.AddMonths(-6),
                        EndDate = DateTime.Now
                    }
                },
                new ReportTemplate
                {
                    Id = "quarterly-summary",
                    Name = "Quarterly Business Summary",
                    Description = "Comprehensive quarterly business performance report",
                    Category = "Executive",
                    DefaultRequest = new ReportRequest
                    {
                        ReportType = ReportType.SalesReport,
                        Format = ReportFormat.PDF,
                        StartDate = DateTime.Now.AddMonths(-3),
                        EndDate = DateTime.Now
                    }
                }
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose resources if needed
            }
            base.Dispose(disposing);
        }
    }

    // View Models
    public class ReportBuilderViewModel
    {
        public List<ReportType> AvailableReportTypes { get; set; }
        public List<ReportFormat> SupportedFormats { get; set; }
        public ReportRequest Request { get; set; }
        public ReportTemplate SelectedTemplate { get; set; }
    }

    public class ReportTemplate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public ReportRequest DefaultRequest { get; set; }
    }

    public class ReportHistoryItem
    {
        public int Id { get; set; }
        public ReportType ReportType { get; set; }
        public ReportFormat Format { get; set; }
        public DateTime GeneratedDate { get; set; }
        public string GeneratedBy { get; set; }
        public string Status { get; set; }
        public string FileName { get; set; }
        public long? FileSizeBytes { get; set; }
    }
}
