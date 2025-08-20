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
        Invoices.DataSource = new DatabaseTable().Select("SELECT InvoiceId, FullName, ContactNumber, Total, DateCreated FROM Invoices ORDER BY DateCreated");
        Invoices.DataBind();
    }

    protected void Invoices_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName == "Edit")
        {
            Response.Redirect("/pos/invoices/view?id=" + e.CommandArgument.ToString());
        }
    }
}