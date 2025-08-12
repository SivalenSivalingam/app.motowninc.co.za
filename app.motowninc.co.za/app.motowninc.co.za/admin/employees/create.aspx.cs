using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.UI;

public partial class admin_employees_create : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/employees/dashboard");
    }

    protected void Create_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(FullName.Text))
        {
            ((admin_admin)Page.Master).Alert("Please Enter Full Name");
            return;
        }

        if (AccountType.SelectedItem.Value == "0")
        {
            ((admin_admin)Page.Master).Alert("Please Select Account Type");
            return;
        }

        try
        {
            var employeeId = Guid.NewGuid().ToString();
            var result = new DatabaseTable().InsertWithPassword("Employees",
                new List<MySqlParameter> {
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmployeeId", Value = employeeId },
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@FullName", Value = FullName.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmailAddress", Value = Guid.NewGuid().ToString().Replace("-", "")},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Password", Value = Guid.NewGuid().ToString().Replace("-", "")},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@AccountType", Value = AccountType.SelectedItem.Value},
                });

            if (result.Item1)
            {
                Response.Redirect("/admin/employees/edit?id=" + employeeId);
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
}