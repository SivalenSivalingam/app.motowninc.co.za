<%@ WebHandler Language="C#" Class="InvoiceHandler" %>

using System.Web;
using MigraDoc.Rendering;
using System.IO;

public class InvoiceHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        var document = new PDF().CreateInvoice(context.Request.QueryString["id"].ToString());

        var pdfRenderer = new PdfDocumentRenderer(true);
        pdfRenderer.Document = document.Item1;
        pdfRenderer.RenderDocument();

        MemoryStream stream = new MemoryStream();
        pdfRenderer.Save(stream, false);

        context.Response.Buffer = true;
        context.Response.Charset = "";
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.ContentType = "application/pdf";
        context.Response.BinaryWrite(stream.ToArray());
        context.Response.Flush();
        context.Response.End();
    }

    public bool IsReusable {
        get {
            return false;
        }
    }
}