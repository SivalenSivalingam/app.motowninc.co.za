using System;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_invoices_dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
            ToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadDashboard();
        }
    }

    private void LoadDashboard()
    {
        Invoices.DataSource = new Reports().Invoices(DateTime.Parse(GetDate(FromDate.Text)), DateTime.Parse(GetDate(ToDate.Text)));
        Invoices.DataBind();
    }

    private string GetDate(string date)
    {
        if (string.IsNullOrEmpty(date))
            return null;

        return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
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

    protected void Search_Click(object sender, EventArgs e)
    {
        LoadDashboard();
    }
}