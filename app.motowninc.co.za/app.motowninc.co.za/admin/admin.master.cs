using AjaxControlToolkit;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class admin_admin : MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;

        if (!Page.IsPostBack)
        {
            SessionCheck();
        }
    }

    public void SessionCheck()
    {
        if (Request.Cookies[ConfigurationManager.AppSettings["SessionCookie"].ToString()] != null)
        {
            var result = IsSessionTokenValid(Request.Cookies[ConfigurationManager.AppSettings["SessionCookie"].ToString()].Value);

            if (!result)
            {
                Response.Redirect("/login");
            }

            if (string.IsNullOrEmpty(Session["SessionEmployeeId"] as string))
            {
                Response.Redirect("/login");
            }
        }
        else
        {
            Response.Redirect("/login");
        }
    }

    protected void PopUpClose_Click(object sender, EventArgs e)
    {
        PopUp.Hide();
    }

    public void Alert(string message, string type = "danger")
    {
        var modalPopupExtender = Page.Master.FindControl("PopUp") as ModalPopupExtender;
        var div = Page.Master.FindControl("PopUpDiv") as HtmlGenericControl;
        div.InnerHtml = "<div class='alert alert alert-" + type + " bg-" + type + " text-light border-0' role='alert'>" + message + "</div>";
        modalPopupExtender.Show();
    }

    private bool IsSessionTokenValid(string token)
    {
        var IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["IssuerSigningKey"]));

        var TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = ConfigurationManager.AppSettings["ValidIssuer"],
            ValidAudience = ConfigurationManager.AppSettings["ValidAudience"],
            LifetimeValidator = LifetimeValidator,
            IssuerSigningKey = IssuerSigningKey
        };

        SecurityToken validatedToken = null;

        try
        {
            new JwtSecurityTokenHandler().ValidateToken(token, TokenValidationParameters, out validatedToken);

            Session session = new Session();

            var claim = ((JwtSecurityToken)validatedToken).Claims.Where(c => session.DecryptString(c.Type) == "Admin").FirstOrDefault();

            if (!string.IsNullOrEmpty(claim.Value))
            {
                Session["SessionEmployeeId"] = session.DecryptString(claim.Value.ToString());
            }
            else
            {
                Response.Redirect("/login");
            }
        }
        catch
        {
            Response.Redirect("/login");
        }

        return validatedToken != null;
    }

    private bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
    {
        return expires != null ? ((DateTime.UtcNow < expires) ? true : false) : false;
    }
}
