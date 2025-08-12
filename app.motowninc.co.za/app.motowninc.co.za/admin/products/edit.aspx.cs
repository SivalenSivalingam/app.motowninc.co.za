using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

public partial class admin_products_edit : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadProduct();
        }
    }

    private void LoadProduct()
    {
        DataTable dataTable = new DatabaseTable().Select("SELECT * FROM Products WHERE ProductId = @ProductId", new List<MySqlParameter> {
                                new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@ProductId", Value = Guid.Parse(Request.QueryString["id"])}});

        if (dataTable.Rows.Count <= 0)
            Response.Redirect("/admin/products/dashboard");

        DataRow product = dataTable.Rows[0];

        Code.Text = product["Code"].ToString();
        Type.Text = product["Type"].ToString();
        Name.Text = product["Name"].ToString();
        Description.Text = product["Description"].ToString();
        Quantity.Text = product["Quantity"].ToString();
        Price.Text = product["Price"].ToString();
        Discount.Text = product["Discount"].ToString();
        Barcode.Text = product["Barcode"].ToString();
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        var result = new DatabaseTable().Update("Products", new List<MySqlParameter> {
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@ProductId", Value = Request.QueryString["id"]},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Code", Value = Code.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Type", Value = Type.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Name", Value = Name.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.LongText, ParameterName="@Description", Value = Description.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.Int32, ParameterName="@Quantity", Value = Quantity.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.Decimal, ParameterName="@Price", Value = Price.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.Decimal, ParameterName="@Discount", Value = Discount.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Barcode", Value = Barcode.Text}
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