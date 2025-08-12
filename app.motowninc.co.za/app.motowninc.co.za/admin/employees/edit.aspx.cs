using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;


public partial class admin_employees_edit : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadEmployee();
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
        NickName.Text = employee["NickName"].ToString();
        EmailAddress.Text = employee["EmailAddress"].ToString();
        AccountType.SelectedValue = employee["AccountType"].ToString();

        ContactNumber.Text = employee["ContactNumber"].ToString();
        Address.Text = employee["Address"].ToString();
        IDNumber.Text = employee["IDNumber"].ToString();
        PlatformAccess.Checked = employee["PlatformAccess"].ToString() == "1";
        Active.Checked = employee["Active"].ToString() == "1";

        DateEngaged.Text = employee["DateEngaged"].ToString();
        EmployeeNumber.Text = employee["EmployeeNumber"].ToString();
        Department.Text = employee["Department"].ToString();
        Position.Text = employee["Position"].ToString();
        LineManager.Text = employee["LineManager"].ToString();
        PayDay.Text = employee["PayDay"].ToString();
        IDNumber.Text = employee["IDNumber"].ToString();
        Rate.Text = employee["Rate"].ToString();
        Hours.Text = employee["Hours"].ToString();
        LOARate.Text = employee["LOARate"].ToString();

        EmergencyContactFullName.Text = employee["EmergencyContactFullName"].ToString();
        EmergencyContactRelationship.Text = employee["EmergencyContactRelationship"].ToString();
        EmergencyContactContactNumber.Text = employee["EmergencyContactContactNumber"].ToString();

        BankAccountBankName.Text = employee["BankAccountBankName"].ToString();
        BankAccountBranchCode.Text = employee["BankAccountBranchCode"].ToString();
        BankAccountAccountName.Text = employee["BankAccountAccountName"].ToString();
        BankAccountAccountNumber.Text = employee["BankAccountAccountNumber"].ToString();
        BankAccountAccountType.Text = employee["BankAccountAccountType"].ToString();
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

        //if (!new Session().IsEmailUnique(EmailAddress.Text.Trim().ToLower()))
        //{
        //    ((admin_admin)Page.Master).Alert("Email address is not unique. Please use a different email.");
        //    return;
        //}

        var result = new DatabaseTable().Update("Employees", new List<MySqlParameter>{
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmployeeId", Value = Guid.Parse(Request.QueryString["id"].ToString())},
                        
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@FullName", Value = FullName.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@NickName", Value = NickName.Text},
                        // new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmailAddress", Value = EmailAddress.Text.Trim().ToLower()},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@AccountType", Value = AccountType.SelectedItem.Value},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@ContactNumber", Value = ContactNumber.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Address", Value = Address.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@IDNumber", Value = IDNumber.Text},

                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@DateEngaged", Value = DateEngaged.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmployeeNumber", Value = EmployeeNumber.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Department", Value = Department.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Position", Value = Position.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@LineManager", Value = LineManager.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@PayDay", Value = PayDay.Text},

                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Rate", Value = GetDecimal(Rate.Text)},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Hours", Value = GetDecimal(Hours.Text)},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@LOARate", Value = GetDecimal(LOARate.Text)},

                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmergencyContactFullName", Value = EmergencyContactFullName.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmergencyContactRelationship", Value = EmergencyContactRelationship.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmergencyContactContactNumber", Value = EmergencyContactContactNumber.Text},

                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@BankAccountBankName", Value = BankAccountBankName.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@BankAccountBranchCode", Value = BankAccountBranchCode.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@BankAccountAccountName", Value = BankAccountAccountName.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@BankAccountAccountNumber", Value = BankAccountAccountNumber.Text},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@BankAccountAccountType", Value = BankAccountAccountType.Text},

                        new MySqlParameter() { MySqlDbType = MySqlDbType.Bit, ParameterName="@PlatformAccess", Value = PlatformAccess.Checked},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.Bit, ParameterName="@Active", Value = Active.Checked},
                        
        });

        //if (!string.IsNullOrEmpty(Password.Text))
        //{
        //    new DatabaseTable().UpdatePassword("Employees", "EmployeeId", Guid.Parse(Request.QueryString["id"].ToString()).ToString(), Password.Text);
        //}

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