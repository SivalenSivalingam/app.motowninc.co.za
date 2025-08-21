using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;

public partial class admin_reports_end_of_day : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadReport();
        }
    }

    private void LoadReport()
    {
        DataSet dataSet = new Reports().EndOfDay(DateTime.Parse(GetDate(Date.Text)));

        Report1.DataSource = dataSet.Tables[0];
        Report1.DataBind();

        Report2.DataSource = dataSet.Tables[1];
        Report2.DataBind();

        Report3.DataSource = dataSet.Tables[2];
        Report3.DataBind();

        Report4.DataSource = dataSet.Tables[3];
        Report4.DataBind();
    }

    private string GetDate(string date)
    {
        if (string.IsNullOrEmpty(date))
            return null;

        return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
    }

    public string State(string invoiceId, string cancelled, string returnedQuantity)
    {
        if (cancelled == "0" && returnedQuantity == "0")
            return invoiceId;

        if (cancelled == "1" && returnedQuantity == "0")
            return invoiceId += "&nbsp;<span class='badge bg-danger'>C</span>";

        if (cancelled == "1" && returnedQuantity == "1")
            return invoiceId += "&nbsp;<span class='badge bg-warning'>C-RQ</span>";

        return invoiceId;
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
}