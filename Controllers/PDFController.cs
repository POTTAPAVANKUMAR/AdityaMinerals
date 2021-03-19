using System;
using AdityaMinerals.BussinessLayer;
using AdityaMinerals.DataAccessLayer;
using AdityaMinerals.EntityModels;
using AdityaMinerals.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using SelectPdf;
using System.IO;

namespace AdityaMinerals.Controllers
{
    public class PDFController : Controller
    {
        public ActionResult BillPdf(string Invoiceid)
        {
            if (Session["UserName"] != null)
            {
                return File(GeneratePdfReport(Invoiceid), MediaTypeNames.Application.Pdf, "AM-" + Invoiceid.ToUpper() + ".pdf");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [NonAction]
        private byte[] GeneratePdfReport(string invoiceid)
        {           
            byte[] result = null;
            int Invoiceid = Convert.ToInt32(invoiceid);
            HtmlToPdf objPdfSelect = new HtmlToPdf();
            var htmlurl = "";
            try
            {
                htmlurl = "http://localhost:44324/PDF/Report?Invoiceid=" + Invoiceid;
                objPdfSelect.Options.MarginTop = 5;
                objPdfSelect.Options.MarginBottom = 5;
                objPdfSelect.Options.MarginLeft = 10;
                objPdfSelect.Options.MarginRight = 10;
                objPdfSelect.Options.ViewerPreferences.CenterWindow = true;
                objPdfSelect.Options.ViewerPreferences.DisplayDocTitle = true;
                objPdfSelect.Options.ViewerPreferences.FitWindow = true;
                objPdfSelect.Options.ViewerPreferences.HideMenuBar = true;
                objPdfSelect.Options.ViewerPreferences.HideToolbar = true;
                objPdfSelect.Options.ViewerPreferences.HideWindowUI = true;
                objPdfSelect.Options.ViewerPreferences.PageMode = PdfViewerPageMode.UseNone;
                objPdfSelect.Options.DisplayHeader = true;
                objPdfSelect.Options.DisplayFooter = true;
                objPdfSelect.Header.DisplayOnFirstPage = true;
                PDFDLController obj = new PDFDLController();
                reportmodel output = new reportmodel();
                output = obj.Reportpdf(Invoiceid);
                ViewData["Report"] = output;
                ViewData.Model = ViewData["Report"];
                StringWriter stringWriter = new StringWriter();
                ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, "Report", null);
                ViewContext viewContext = new ViewContext(
                        ControllerContext,
                        viewResult.View,
                        new ViewDataDictionary(ViewData.Model),
                        new TempDataDictionary(),
                        stringWriter
                        );
                viewResult.View.Render(viewContext, stringWriter);
                string htmlToConvert = stringWriter.ToString();
                SelectPdf.PdfDocument pdffile = objPdfSelect.ConvertHtmlString(htmlToConvert);
                result = pdffile.Save();
                pdffile.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public ActionResult Report(int Invoiceno)
        {
            try
            {
                if (Session["UserName"] != null)
                {
                    PDFDLController obj = new PDFDLController();
                    reportmodel output = new reportmodel();
                    output = obj.Reportpdf(Invoiceno);
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}