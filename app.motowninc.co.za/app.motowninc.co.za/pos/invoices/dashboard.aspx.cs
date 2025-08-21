using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pos_invoices_dashboard : Page
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
        Invoices.DataSource = new Reports().Invoices(DateTime.Now.AddMonths(-1), DateTime.Now);
        Invoices.DataBind();
    }

    protected void Invoices_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName == "Edit")
        {
            Response.Redirect("/pos/invoices/view?id=" + e.CommandArgument.ToString());
        }
    }

    public string State(string invoiceId, string cancelled, string returnedQuantity)
    {
        if (cancelled == "0" && returnedQuantity == "0")
            return invoiceId;

        if (cancelled == "1" && returnedQuantity == "0")
            return invoiceId += "&nbsp;<span class='badge bg-danger'>C</span>";

        if (cancelled == "1" && returnedQuantity == "1")
            return invoiceId += "&nbsp;<span class='badge bg-warning'>C-RQ</span>";

        return invoiceId;
    }
}