using System;
using System.Web.UI;

public partial class error : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["url"] != null)
            Url.Text = "Url : " + Request.QueryString["url"].ToString();

        if (Request.QueryString["message"] != null)
            Message.Text = "Error : " + Request.QueryString["message"].ToString();
    }
}