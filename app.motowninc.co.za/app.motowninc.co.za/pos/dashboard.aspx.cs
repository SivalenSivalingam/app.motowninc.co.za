using Dapper;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        DataSet data = new DatabaseTable().SQL(sql.ToString(), new List<MySqlParameter> {
                                new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@EmployeeId", Value = Session["SessionEmployeeId"].ToString()},
                           });

        Customers.DataSource = data.Tables[0];
        Customers.DataBind();

        Products.DataSource = data.Tables[1];
        Products.DataBind();

        LoadCart();
    }

    private void LoadCart()
    {
        DataTable dataTable = new DatabaseTable().Select("SELECT Code, Type, Name, Description, Cart.Quantity, Price, Discount, Cart.ProductId FROM Cart INNER JOIN Products ON Cart.ProductId = Products.ProductId WHERE Cart.EmployeeId = @EmployeeId ORDER BY Cart.DateCreated;", new List<MySqlParameter> {
                              new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@EmployeeId", Value = Session["SessionEmployeeId"].ToString()},
                           });

        Cart.DataSource = dataTable;
        Cart.DataBind();

        decimal subtotal = 0;
        decimal discount = 0;

        foreach (DataRow dataRow in dataTable.Rows)
        {
            subtotal += decimal.Parse(dataRow["Quantity"].ToString()) * (decimal.Parse(dataRow["Price"].ToString()) - decimal.Parse(dataRow["Discount"].ToString()));
            discount += decimal.Parse(dataRow["Quantity"].ToString()) * decimal.Parse(dataRow["Discount"].ToString());
        }

        Subtotal.Text = subtotal.ToString("N2");
        Discount.Text = discount.ToString("N2");

        decimal total = subtotal - discount;
        decimal vat = total * (decimal)0.15;
        VAT.Text = vat.ToString("N2");
        Total.Text = (total + vat).ToString("N2");
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
                new Cart().OverrideCartQuantity(Session["SessionEmployeeId"].ToString(), e.CommandArgument.ToString(), decimal.Parse((e.Item.FindControl("Quantity") as TextBox).Text));
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

    private decimal GetDecimal(string value)
    {
        value = Regex.Replace(value, @"[a-zA-Z\s]", "");

        if (string.IsNullOrEmpty(value))
            return 0;

        return decimal.Parse(value.Trim().ToString().Replace(",", "."), CultureInfo.InvariantCulture);
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        var InvoiceId = Guid.NewGuid().ToString();

        var result = new DatabaseTable().Insert("Invoices",
               new List<MySqlParameter> {
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@InvoiceId", Value = InvoiceId },
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmployeeId", Value = Session["SessionEmployeeId"].ToString()},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@FullName", Value = FullName.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@ContactNumber", Value = ContactNumber.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmailAddress", Value = EmailAddress.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@PaymentType", Value = PaymentType.SelectedItem.Value},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.Decimal, ParameterName="@Total", Value = GetDecimal(Total.Text)},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.Decimal, ParameterName="@CashReceived", Value = GetDecimal(CashReceived.Text)},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.Decimal, ParameterName="@CashReturned", Value = GetDecimal(CashReturned.Text)},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Make", Value = Make.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Model", Value = Model.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Mileage", Value = Mileage.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@LicensePlate", Value = LicensePlate.Text},
                    new MySqlParameter() { MySqlDbType = MySqlDbType.LongText, ParameterName="@Note", Value = Note.Text},
               });

        if(!result.Item1)
        {
            ((pos_pos)Page.Master).Alert(result.Item2);
            return;
        }

        Cart cart = new Cart();

        DataTable cartInvoiceItems = cart.Get(Session["SessionEmployeeId"].ToString());

        List<InvoiceItem> invoiceItems = new List<InvoiceItem>();

        foreach (DataRow dataRow in cartInvoiceItems.Rows)
        {
            invoiceItems.Add(new InvoiceItem()
            {
                InvoiceItemId = Guid.NewGuid().ToString(),
                InvoiceId = InvoiceId,
                ProductId = dataRow["ProductId"].ToString(),
                Code = dataRow["Code"].ToString(),
                Type = dataRow["Type"].ToString(),
                Name = dataRow["Name"].ToString(),
                Description = dataRow["Description"].ToString(),
                Quantity = decimal.Parse(dataRow["Quantity"].ToString()),
                Price = decimal.Parse(dataRow["Price"].ToString()),
                Discount = decimal.Parse(dataRow["Discount"].ToString()),
            });
        }

        using (var db = new MySqlConnection(new Repository().GetMySqlConnection()))
        {
            string sql = @"
                INSERT INTO InvoiceItems (
                    InvoiceItemId, InvoiceId, ProductId, Code, Type, Name, Description, Quantity, Price, Discount
                )
                VALUES (
                    @InvoiceItemId, @InvoiceId, @ProductId, @Code, @Type, @Name, @Description, @Quantity, @Price, @Discount
                )";

            int rowsAffected = db.Execute(sql, invoiceItems);
        }


        cart.ClearCart(Session["SessionEmployeeId"].ToString());
        LoadCart();
        Response.Redirect("/pos/invoices/view?id=" + InvoiceId);
    }

    protected void ClearCart_Click(object sender, EventArgs e)
    {
        new Cart().ClearCart(Session["SessionEmployeeId"].ToString());
        LoadCart();
    }
}