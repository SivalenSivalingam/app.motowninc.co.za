using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pos_dashboard : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ((pos_pos)Page.Master).SessionCheck();

        if (!IsPostBack)
        {
            LoadDropdownOptions();
            GetData();
        }
    }

    private void LoadDropdownOptions()
    {
        PaymentType.Items.Clear();
        PaymentType.Items.Insert(0, new ListItem("Please Select A Option", "Please Select A Option"));

        var sortedList = new DropdownOptions().PaymentTypes.OrderBy(x => x).ToList();

        for (int index = 1; index <= sortedList.Count; index++)
        {
            PaymentType.Items.Insert(index, new ListItem(sortedList[index - 1], sortedList[index - 1]));
        }
    }

    private void GetData()
    {
        StringBuilder sql = new StringBuilder();

        sql.Append("SELECT * FROM Customers ORDER BY Name;");
        sql.Append("SELECT * FROM Products ORDER BY Name;");
        sql.Append("SELECT Code, Type, Name, Description, Cart.Quantity, Price, Discount, Cart.ProductId FROM Cart INNER JOIN Products ON Cart.ProductId = Products.ProductId WHERE Cart.EmployeeId = @EmployeeId ORDER BY Cart.DateCreated;");

        DataSet data = new DatabaseTable().SQL(sql.ToString(), new List<MySqlParameter> {
                                new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@EmployeeId", Value = Session["SessionEmployeeId"].ToString()},
                           });

        Customers.DataSource = data.Tables[0];
        Customers.DataBind();

        Products.DataSource = data.Tables[1];
        Products.DataBind();

        Cart.DataSource = data.Tables[2];
        Cart.DataBind();
    }

    private void LoadCart()
    {
        Cart.DataSource = new DatabaseTable().Select("SELECT Code, Type, Name, Description, Cart.Quantity, Price, Discount, Cart.ProductId FROM Cart INNER JOIN Products ON Cart.ProductId = Products.ProductId WHERE Cart.EmployeeId = @EmployeeId ORDER BY Cart.DateCreated;", new List<MySqlParameter> {
                              new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@EmployeeId", Value = Session["SessionEmployeeId"].ToString()},
                           });
        Cart.DataBind();
    }

    protected void Products_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            new Cart().AddToCart(Session["SessionEmployeeId"].ToString(), e.CommandArgument.ToString(), 1);
            LoadCart();
        }
    }

    protected void Customers_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName == "Select")
        {
            CustomerSearchModal.Hide();


            DataTable dataTable = new DatabaseTable().Select("SELECT * FROM Customers WHERE CustomerId = @CustomerId", new List<MySqlParameter> {
                                new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@CustomerId", Value = e.CommandArgument.ToString()}});

            DataRow customer = dataTable.Rows[0];

            FullName.Text = customer["Name"].ToString();
            ContactNumber.Text = customer["ContactNumber"].ToString();
            EmailAddress.Text = customer["EmailAddress"].ToString();
        }
    }

    protected void Cart_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch(e.CommandName)
        {
            case "Update":
                new Cart().OverrideCartQuantity(Session["SessionEmployeeId"].ToString(), e.CommandArgument.ToString(), int.Parse((e.Item.FindControl("Quantity") as TextBox).Text));
                break;
            case "Delete":
                new Cart().Delete(Session["SessionEmployeeId"].ToString(), e.CommandArgument.ToString());
                break;
        }

        LoadCart();
    }

    protected void CustomerSearch_Click(object sender, EventArgs e)
    {
        CustomerSearchModal.Show();
    }

    protected void CustomerSearchPanelPopUpClose_Click(object sender, EventArgs e)
    {
        CustomerSearchModal.Hide();
    }

    protected void Product_TextChanged(object sender, EventArgs e)
    {

    }
}