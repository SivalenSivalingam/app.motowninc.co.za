using System;
using System.Data;
using System.Text;
using System.Web.UI;

public partial class pos_dashboard : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetData();
        }
    }

    private void GetData()
    {
        StringBuilder sql = new StringBuilder();

        sql.Append("SELECT * FROM Customers ORDER BY Name;");
        sql.Append("SELECT * FROM Products ORDER BY Name;");

        DataSet data = new DatabaseTable().SQL(sql.ToString());

        Customers.DataSource = data.Tables[0];
        Customers.DataBind();

        Products.DataSource = data.Tables[1];
        Products.DataBind();
    }

    protected void Products_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {

    }

    protected void Customers_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {

    }

    protected void Cart_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {

    }
}