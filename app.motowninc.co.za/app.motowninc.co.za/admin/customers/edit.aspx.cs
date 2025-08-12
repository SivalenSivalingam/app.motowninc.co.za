using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_customers_edit : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDropdownOptions();
            LoadCustomer();
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

    private void LoadCustomer()
    {
        DataTable dataTable = new DatabaseTable().Select("SELECT * FROM Customers WHERE CustomerId = @CustomerId", new List<MySqlParameter> {
                                new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@CustomerId", Value = Guid.Parse(Request.QueryString["id"])}});

        if (dataTable.Rows.Count <= 0)
            Response.Redirect("/admin/customers/dashboard");

        DataRow customer = dataTable.Rows[0];

        Name.Text = customer["Name"].ToString();
        ContactNumber.Text = customer["ContactNumber"].ToString();
        EmailAddress.Text = customer["EmailAddress"].ToString();
        AccountType.SelectedItem.Text = customer["AccountType"].ToString();
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        var result = new DatabaseTable().Update("Customers", new List<MySqlParameter> {
                         new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@CustomerId", Value = Request.QueryString["id"]},
                         new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Name", Value = Name.Text},
                         new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@ContactNumber", Value = ContactNumber.Text},
                         new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmailAddress", Value = EmailAddress.Text},
                         new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@AccountType", Value = AccountType.SelectedItem.Text}
                    });

        if (result.Item1)
        {
            ((admin_admin)Page.Master).Alert("Update Successful", "success");
        }
        else
        {
            ((admin_admin)Page.Master).Alert(result.Item2);
        }
    }
}