using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_customers_edit : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadEmployees();
            LoadCustomer();
        }
    }

    private void LoadEmployees()
    {
        DataTable dataTable = new DatabaseTable().Select("SELECT EmployeeId, FullName FROM Employees WHERE AccountType = 'Employee' ORDER BY FullName");

        Employees.Items.Clear();
        Employees.DataSource = dataTable;
        Employees.DataTextField = "FullName";
        Employees.DataValueField = "EmployeeId";
        Employees.DataBind();
        Employees.Items.Insert(0, new ListItem("Please Select Employee", "0"));
    }

    private void LoadCustomer()
    {
        DataTable dataTable = new DatabaseTable().Select("SELECT * FROM Customers WHERE CustomerId = @CustomerId", new List<MySqlParameter> {
                                new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@CustomerId", Value = Guid.Parse(Request.QueryString["id"])}});

        if (dataTable.Rows.Count <= 0)
            Response.Redirect("/admin/customers/dashboard");

        DataRow customer = dataTable.Rows[0];

        CompanyName.Text = customer["CompanyName"].ToString();
        FullName.Text = customer["FullName"].ToString();
        EmailAddress.Text = customer["EmailAddress"].ToString();
        MobileNumber.Text = customer["MobileNumber"].ToString();
        LandlineNumber.Text = customer["LandlineNumber"].ToString();

        SecondaryFullName.Text = customer["SecondaryFullName"].ToString();
        SecondaryEmailAddress.Text = customer["SecondaryEmailAddress"].ToString();
        SecondaryMobileNumber.Text = customer["SecondaryMobileNumber"].ToString();
        SecondaryLandlineNumber.Text = customer["SecondaryLandlineNumber"].ToString();

        PhysicalAddress.Text = customer["PhysicalAddress"].ToString();
        PhysicalAddressCity.Text = customer["PhysicalAddressCity"].ToString();
        PhysicalAddressProvince.Text = customer["PhysicalAddressProvince"].ToString();
        PhysicalAddressCountry.Text = customer["PhysicalAddressCountry"].ToString();
        PhysicalAddressPostalCode.Text = customer["PhysicalAddressPostalCode"].ToString();

        ShippingAddress.Text = customer["ShippingAddress"].ToString();
        ShippingAddressCity.Text = customer["ShippingAddressCity"].ToString();
        ShippingAddressProvince.Text = customer["ShippingAddressProvince"].ToString();
        ShippingAddressCountry.Text = customer["ShippingAddressCountry"].ToString();
        ShippingAddressPostalCode.Text = customer["ShippingAddressPostalCode"].ToString();

        if (!string.IsNullOrEmpty(customer["EmployeeId"].ToString()))
            Employees.SelectedValue = customer["EmployeeId"].ToString();

    }

    protected void Update_Click(object sender, EventArgs e)
    {
        var result = new DatabaseTable().Update("Customers", new List<MySqlParameter> {
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@CustomerId", Value = Request.QueryString["id"]},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmployeeId", Value = Employees.SelectedItem.Value == "0" ? null : Employees.SelectedItem.Value },
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@CompanyName", Value = CompanyName.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@FullName", Value = FullName.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@MobileNumber", Value = MobileNumber.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@LandlineNumber", Value = LandlineNumber.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmailAddress", Value = EmailAddress.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@SecondaryFullName", Value = SecondaryFullName.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@SecondaryMobileNumber", Value = SecondaryMobileNumber.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@SecondaryLandlineNumber", Value = SecondaryLandlineNumber.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@SecondaryEmailAddress", Value = SecondaryEmailAddress.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@PhysicalAddress", Value = PhysicalAddress.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@PhysicalAddressCity", Value = PhysicalAddressCity.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@PhysicalAddressProvince", Value = PhysicalAddressProvince.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@PhysicalAddressCountry", Value = PhysicalAddressCountry.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@PhysicalAddressPostalCode", Value = PhysicalAddressPostalCode.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@ShippingAddress", Value = ShippingAddress.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@ShippingAddressCity", Value = ShippingAddressCity.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@ShippingAddressProvince", Value = ShippingAddressProvince.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@ShippingAddressCountry", Value = ShippingAddressCountry.Text},
                       new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@ShippingAddressPostalCode", Value = ShippingAddressPostalCode.Text},
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