using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_customers_dashboard : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadCustomers();
        }
    }

    private void LoadCustomers()
    {
        Customers.DataSource = new DatabaseTable().Select("SELECT * FROM Customers ORDER BY Name");
        Customers.DataBind();
    }

    protected void CreateCustomer_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/customers/create");
    }

    protected void Customers_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("/admin/customers/edit?id=" + e.CommandArgument.ToString());
        }
    }
}