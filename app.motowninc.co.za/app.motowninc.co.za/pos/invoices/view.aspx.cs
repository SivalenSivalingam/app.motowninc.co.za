using System;
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

    }
}