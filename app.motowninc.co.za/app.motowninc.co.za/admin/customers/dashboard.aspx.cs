using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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
        DataTable dataTable = new DatabaseTable().Select("SELECT Employees.FullName AS Employee, Customers.* FROM Customers LEFT OUTER JOIN Employees ON Employees.EmployeeId = Customers.EmployeeId ORDER BY CompanyName, FullName;");
        Customers.DataSource = dataTable;
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