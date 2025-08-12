using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_customers_create : Page
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

        var sortedList = new DropdownOptions().CustomerAccountTypes.OrderBy(x => x).ToList();

        for (int index = 1; index <= sortedList.Count; index++)
        {
            AccountType.Items.Insert(index, new ListItem(sortedList[index - 1], sortedList[index - 1]));
        }
    }

    protected void Create_Click(object sender, EventArgs e)
    {
        try
        {
            var customerId = Guid.NewGuid().ToString();
            var result = new DatabaseTable().Insert("Customers",
                new List<MySqlParameter> {
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@CustomerId", Value = customerId},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Name", Value = Name.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@ContactNumber", Value = ContactNumber.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmailAddress", Value = EmailAddress.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@AccountType", Value = AccountType.SelectedItem.Value}
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