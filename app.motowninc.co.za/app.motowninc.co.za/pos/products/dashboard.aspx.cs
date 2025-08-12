using System;
using System.Web.UI;

public partial class pos_products_dashboard : Page
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
}