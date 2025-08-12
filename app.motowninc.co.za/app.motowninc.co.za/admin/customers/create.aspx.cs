using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.UI;

public partial class admin_customers_create : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Create_Click(object sender, EventArgs e)
    {
        try
        {
            var customerId = Guid.NewGuid().ToString();
            var result = new DatabaseTable().Insert("Customers",
                new List<MySqlParameter> {
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@CustomerId", Value = customerId},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmployeeId", Value = Session["SessionEmployeeId"].ToString()},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@CompanyName", Value = CompanyName.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@FullName", Value = FullName.Text}
                });

            if (result.Item1)
            {
                Response.Redirect("/admin/customers/edit?id=" + customerId);
            }
            else
            {
                ((admin_admin)Page.Master).Alert(result.Item2);
            }
        }
        catch (Exception exception)
        {
            ((admin_admin)Page.Master).Alert(exception.Message);
        }
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/customers/dashboard");
    }
}