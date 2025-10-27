using System;
using System.Collections.Generic;
using System.Linq;
using AdityaMinerals.EntityModels;
using AdityaMinerals.Models;

namespace AdityaMinerals.DataAccessLayer
{
    public class EnhancedReportDL
    {
        private readonly AdityamineralsEntities _context;

        public EnhancedReportDL()
        {
            _context = new AdityamineralsEntities();
        }

        public reportmodel GetInvoiceData(int invoiceId)
        {
            try
            {
                var invoiceHeader = _context.ADM_L_BILLINGPART1
                    .FirstOrDefault(x => x.InvoiceNo == invoiceId);

                var invoiceItems = _context.ADM_L_BILLINGPART2
                    .Where(x => x.InvoiceNo == invoiceId)
                    .ToList();

                var products = _context.ADM_M_BILLINGPRODUCTS.ToList();

                return new reportmodel
                {
                    bp1 = invoiceHeader,
                    bp2 = invoiceItems,
                    bp = products
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving invoice data: {ex.Message}", ex);
            }
        }

        public SalesDataResult GetSalesData(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var query = _context.ADM_L_BILLINGPART1.AsQueryable();

                if (startDate.HasValue)
                {
                    query = query.Where(x => x.DateOfIssue >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(x => x.DateOfIssue <= endDate.Value);
                }

                var invoices = query.ToList();
                var invoiceIds = invoices.Select(x => x.InvoiceNo).ToList();

                var invoiceItems = _context.ADM_L_BILLINGPART2
                    .Where(x => invoiceIds.Contains(x.InvoiceNo))
                    .ToList();

                return new SalesDataResult
                {
                    Invoices = invoices,
                    InvoiceItems = invoiceItems
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving sales data: {ex.Message}", ex);
            }
        }

        public List<ADM_M_BILLINGPRODUCTS> GetAllProducts()
        {
            try
            {
                return _context.ADM_M_BILLINGPRODUCTS.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving products: {ex.Message}", ex);
            }
        }

        public CustomerDataResult GetCustomerData(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var query = _context.ADM_L_BILLINGPART1.AsQueryable();

                if (startDate.HasValue)
                {
                    query = query.Where(x => x.DateOfIssue >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(x => x.DateOfIssue <= endDate.Value);
                }

                var invoices = query.ToList();
                var invoiceIds = invoices.Select(x => x.InvoiceNo).ToList();

                var invoiceItems = _context.ADM_L_BILLINGPART2
                    .Where(x => invoiceIds.Contains(x.InvoiceNo))
                    .ToList();

                return new CustomerDataResult
                {
                    Invoices = invoices,
                    InvoiceItems = invoiceItems
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving customer data: {ex.Message}", ex);
            }
        }

        public List<ADM_M_UOM> GetAllUOMs()
        {
            try
            {
                return _context.ADM_M_UOM.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving UOMs: {ex.Message}", ex);
            }
        }

        public List<ADM_M_USER> GetAllUsers()
        {
            try
            {
                return _context.ADM_M_USER.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving users: {ex.Message}", ex);
            }
        }

        // Helper method to get invoice summary statistics
        public InvoiceSummaryStats GetInvoiceSummaryStats(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var query = _context.ADM_L_BILLINGPART1.AsQueryable();

                if (startDate.HasValue)
                {
                    query = query.Where(x => x.DateOfIssue >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(x => x.DateOfIssue <= endDate.Value);
                }

                var invoices = query.ToList();
                var invoiceIds = invoices.Select(x => x.InvoiceNo).ToList();

                var invoiceItems = _context.ADM_L_BILLINGPART2
                    .Where(x => invoiceIds.Contains(x.InvoiceNo))
                    .ToList();

                return new InvoiceSummaryStats
                {
                    TotalInvoices = invoices.Count,
                    TotalAmount = invoiceItems.Sum(x => x.Amount ?? 0),
                    TotalDiscount = invoiceItems.Sum(x => x.Discount ?? 0),
                    TotalNetAmount = invoiceItems.Sum(x => x.ValueofSupply ?? 0),
                    UniqueCustomers = invoices.Select(x => x.BP_Name_VC).Distinct().Count(),
                    UniqueProducts = invoiceItems.Select(x => x.ProductionDescription_VC).Distinct().Count()
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving invoice summary stats: {ex.Message}", ex);
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }

    // Helper classes for data results
    public class SalesDataResult
    {
        public List<ADM_L_BILLINGPART1> Invoices { get; set; }
        public List<ADM_L_BILLINGPART2> InvoiceItems { get; set; }
    }

    public class CustomerDataResult
    {
        public List<ADM_L_BILLINGPART1> Invoices { get; set; }
        public List<ADM_L_BILLINGPART2> InvoiceItems { get; set; }
    }

    public class InvoiceSummaryStats
    {
        public int TotalInvoices { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalNetAmount { get; set; }
        public int UniqueCustomers { get; set; }
        public int UniqueProducts { get; set; }
    }
}
