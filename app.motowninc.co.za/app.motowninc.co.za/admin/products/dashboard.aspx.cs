using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_products_dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Create_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/products/create");
    }

    protected void Products_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
}