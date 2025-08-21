using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_invoices_dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDashboard();
        }
    }

    private void LoadDashboard()
    {
        Invoices.DataSource = new DatabaseTable().Select("SELECT InvoiceId, Cancelled, ReturnedQuantity, FullName, ContactNumber, Total, DateCreated FROM Invoices ORDER BY DateCreated");
        Invoices.DataBind();
    }

    protected void Invoices_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("/admin/invoices/view?id=" + e.CommandArgument.ToString());
        }
    }

    public string State(string invoiceId, string cancelled, string returnedQuantity)
    {
        if(cancelled == "0" && returnedQuantity == "0")
            return invoiceId;

        if (cancelled == "1" && returnedQuantity == "0")
            return invoiceId += "&nbsp;<span class='badge bg-danger'>C</span>";

        if (cancelled == "1" && returnedQuantity == "1")
            return invoiceId += "&nbsp;<span class='badge bg-warning'>C-RQ</span>";

        return invoiceId;
    }
}