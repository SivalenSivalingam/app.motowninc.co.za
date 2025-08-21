using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_employees_create : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDropdownOptions();
        }
    }

    private void LoadDropdownOptions()
    {
        AccountType.Items.Clear();
        AccountType.Items.Insert(0, new ListItem("Please Select A Option", "Please Select A Option"));

        var sortedList = new DropdownOptions().EmployeeAccountTypes.OrderBy(x => x).ToList();

        for (int index = 1; index <= sortedList.Count; index++)
        {
            AccountType.Items.Insert(index, new ListItem(sortedList[index - 1], sortedList[index - 1]));
        }
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

        try
        {
            var employeeId = Guid.NewGuid().ToString();
            var result = new DatabaseTable().InsertWithPassword("Employees",
                new List<MySqlParameter> {
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmployeeId", Value = employeeId },
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@FullName", Value = FullName.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@ContactNumber", Value = ContactNumber.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmailAddress", Value = EmailAddress.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.LongText, ParameterName="@Password", Value = Password.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@AccountType", Value = AccountType.SelectedItem.Value},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.Bit, ParameterName="@PlatformAccess", Value = PlatformAccess.Checked},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.Bit, ParameterName="@Active", Value = Active.Checked},
                });

            if (result.Item1)
            {
                Response.Redirect("/admin/employees/dashboard");
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