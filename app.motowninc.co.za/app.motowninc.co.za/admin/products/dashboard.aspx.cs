using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_products_dashboard : Page
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
        Products.DataSource = new DatabaseTable().Select("SELECT * FROM Products ORDER BY Name");
        Products.DataBind();
    }

    protected void Create_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/products/create");
    }

    protected void Products_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("/admin/products/edit?id=" + e.CommandArgument.ToString());
        }
    }
}