using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_employees_edit : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDropdownOptions();
            LoadEmployee();
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

    private void LoadEmployee()
    {
        DataTable dataTale = new DatabaseTable().Select("SELECT * FROM Employees WHERE EmployeeId = @EmployeeId;", new List<MySqlParameter> {
                                new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@EmployeeId", Value = Guid.Parse(Request.QueryString["id"].ToString()).ToString()},
                           });

        if (dataTale.Rows.Count != 1)
            Response.Redirect("/admin/employees/dashboard");

        DataRow employee = dataTale.Rows[0];

        FullName.Text = employee["FullName"].ToString();
        EmailAddress.Text = employee["EmailAddress"].ToString();
        CurrentEmailAddress.Value = employee["EmailAddress"].ToString();
        AccountType.SelectedValue = employee["AccountType"].ToString();
        ContactNumber.Text = employee["ContactNumber"].ToString();
        PlatformAccess.Checked = employee["PlatformAccess"].ToString() == "1";
        Active.Checked = employee["Active"].ToString() == "1";
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/employees/dashboard");
    }

    protected void Update_Click(object sender, EventArgs e)
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

        if (CurrentEmailAddress.Value.Trim().ToLower() != EmailAddress.Text.Trim().ToLower())
        {
            if (!new Session().IsEmailUnique(EmailAddress.Text.Trim().ToLower()))
            {
                ((admin_admin)Page.Master).Alert("Email address is not unique. Please use a different email.");
                return;
            }
        }

        var result = new DatabaseTable().Update("Employees", new List<MySqlParameter>{
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmployeeId", Value = Guid.Parse(Request.QueryString["id"].ToString())},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@FullName", Value = FullName.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@ContactNumber", Value = ContactNumber.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmailAddress", Value = EmailAddress.Text.Trim().ToLower()},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@AccountType", Value = AccountType.SelectedItem.Value},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.Bit, ParameterName="@PlatformAccess", Value = PlatformAccess.Checked},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.Bit, ParameterName="@Active", Value = Active.Checked},

        });

        if (!string.IsNullOrEmpty(Password.Text))
        {
            new DatabaseTable().UpdatePassword("Employees", "EmployeeId", Guid.Parse(Request.QueryString["id"].ToString()).ToString(), Password.Text);
        }

        if (result.Item1)
        {
            ((admin_admin)Page.Master).Alert("Update Successful", "success");
        }
        else
        {
            ((admin_admin)Page.Master).Alert(result.Item2);
        }
    }

    private decimal GetDecimal(string value)
    {
        value = Regex.Replace(value, @"[a-zA-Z]", "");

        if (string.IsNullOrEmpty(value))
            return 0;

        return decimal.Parse(value.Trim().ToString().Replace(",", "."), CultureInfo.InvariantCulture);
    }
}