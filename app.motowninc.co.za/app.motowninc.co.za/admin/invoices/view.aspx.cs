using Dapper;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;

public partial class admin_invoices_view : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PdfViewer.Src = "/admin/invoices/InvoiceHandler.ashx?id=" + Request.QueryString["id"].ToString() + "#toolbar=0";
        }
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        var result = new DatabaseTable().Update("Invoices", new List<MySqlParameter> {
                         new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@InvoiceId", Value = Request.QueryString["id"].ToString()},
                         new MySqlParameter() { MySqlDbType = MySqlDbType.Bit, ParameterName="@Cancelled", Value = true}
                    });

        Response.Redirect("/admin/invoices/dashboard");
    }

    protected void CancelReturnQuantity_Click(object sender, EventArgs e)
    {
        DataTable dataTable = new DatabaseTable().Select("SELECT ProductId, Quantity FROM InvoiceItems WHERE InvoiceId = @InvoiceId;", new List<MySqlParameter> {
                              new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@InvoiceId", Value = Request.QueryString["id"].ToString()},
                           });

        List<InvoiceItem> invoiceItems = new List<InvoiceItem>();

        foreach (DataRow dataRow in dataTable.Rows)
        {
            invoiceItems.Add(new InvoiceItem()
            {
                ProductId = dataRow["ProductId"].ToString(),
                Quantity = decimal.Parse(dataRow["Quantity"].ToString()),
            });
        }

        // Stock Take - Increase Product Quantity
        using (var db = new MySqlConnection(new Repository().GetMySqlConnection()))
        {
            string sql = @"UPDATE Products SET Quantity = Quantity + @Quantity WHERE ProductId = @ProductId";

            db.Execute(sql, invoiceItems);
        }

        var result = new DatabaseTable().Update("Invoices", new List<MySqlParameter> {
                         new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@InvoiceId", Value = Request.QueryString["id"].ToString()},
                         new MySqlParameter() { MySqlDbType = MySqlDbType.Bit, ParameterName="@Cancelled", Value = true},
                         new MySqlParameter() { MySqlDbType = MySqlDbType.Bit, ParameterName="@ReturnedQuantity", Value = true}
                    });

        Response.Redirect("/admin/invoices/dashboard");
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