using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_products_create : Page
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
        ProductType.Items.Clear();
        ProductType.Items.Insert(0, new ListItem("Please Select A Option", "Please Select A Option"));

        var sortedList = new DropdownOptions().ProductTypes.OrderBy(x => x).ToList();

        for (int index = 1; index <= sortedList.Count; index++)
        {
            ProductType.Items.Insert(index, new ListItem(sortedList[index - 1], sortedList[index - 1]));
        }
    }

    protected void Create_Click(object sender, EventArgs e)
    {
        try
        {
            var productId = Guid.NewGuid().ToString();
            var result = new DatabaseTable().Insert("Products",
                new List<MySqlParameter> {
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@ProductId", Value = productId},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Code", Value = Code.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Type", Value = ProductType.SelectedItem.Value},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Name", Value = Name.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.LongText, ParameterName="@Description", Value = Description.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.Int32, ParameterName="@Quantity", Value = Quantity.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.Decimal, ParameterName="@Price", Value = Price.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.Decimal, ParameterName="@Discount", Value = Discount.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Barcode", Value = Barcode.Text}
                });

            if (result.Item1)
            {
                Response.Redirect("/admin/products/edit?id=" + productId);
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
        Response.Redirect("/admin/products/dashboard");
    }
}