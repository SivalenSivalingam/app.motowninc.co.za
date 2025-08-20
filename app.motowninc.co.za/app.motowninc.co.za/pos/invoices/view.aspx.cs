using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.IO;
using System.Web.UI;

public partial class pos_invoices_view : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PdfViewer.Src = "/pos/invoices/InvoiceHandler.ashx?id=" + Request.QueryString["id"].ToString() + "#toolbar=0";
        }
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {

    }

    protected void Download_Click(object sender, EventArgs e)
    {
        var document = new PDF().CreateInvoice(Request.QueryString["id"].ToString());
        Dowload(document.Item1, document.Item2);
    }

    private void Dowload(Document document, string fileName)
    {
        var pdfRenderer = new PdfDocumentRenderer();
        pdfRenderer.Document = document;
        pdfRenderer.RenderDocument();

        MemoryStream stream = new MemoryStream();
        pdfRenderer.Save(stream, false);
        Response.Clear();
        Response.AddHeader("Content-type", "application/pdf");
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
        Response.BinaryWrite(stream.ToArray());
        Response.Flush();
        stream.Close();
        Response.End();
    }
}