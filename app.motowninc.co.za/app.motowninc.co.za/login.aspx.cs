using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Security.Claims;
using System.Web;
using System.Web.UI.HtmlControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Response.Cookies[ConfigurationManager.AppSettings["SessionCookie"].ToString()] != null)
            {
                Response.Cookies[ConfigurationManager.AppSettings["SessionCookie"].ToString()].Expires = DateTime.Now.AddDays(-1);
            }
        }
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(EmailAddress.Text.Trim()))
        {
            Alert("Please enter email address.");
            return;
        }

        if (string.IsNullOrEmpty(Password.Text.Trim()))
        {
            Alert("Please enter password.");
            return;
        }

        Session session = new Session();
        DataTable dataTable = session.GetAccess(EmailAddress.Text.Trim(), Password.Text.Trim());

        if (dataTable.Rows.Count == 1 && EmailAddress.Text.ToLower() == dataTable.Rows[0]["EmailAddress"].ToString().ToLower())
        {
            var token = session.CreateSessionTokenClaims(new List<Claim> {
                new Claim(session.EncryptString(dataTable.Rows[0]["AccountType"].ToString()), session.EncryptString(dataTable.Rows[0]["EmployeeId"].ToString())),
            });

            HttpCookie sessionToken = new HttpCookie(ConfigurationManager.AppSettings["SessionCookie"].ToString())
            {
                Value = token,
                Expires = DateTime.Now.AddMonths(1)
            };

            Response.Cookies.Add(sessionToken);

            switch (dataTable.Rows[0]["AccountType"].ToString())
            {
                case "Admin":
                    Response.Redirect("/admin/dashboard");
                    break;
                case "Till":
                    Response.Redirect("/admin/dashboard");
                    break;
            }
        }
        else
        {
            Alert("Login Failed.");
        }
    }

    protected void PopUpClose_Click(object sender, EventArgs e)
    {
        PopUp.Hide();
    }

    public void Alert(string message, string type = "danger")
    {
        var div = Page.FindControl("PopUpDiv") as HtmlGenericControl;
        div.InnerHtml = "<div class='alert alert alert-" + type + " bg-" + type + " text-light border-0' role='alert'>" + message + "</div>";
        PopUp.Show();
    }
}