using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_employees_dashboard : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadEmployees();
        }
    }

    private void LoadEmployees()
    {
        Employees.DataSource = new Employees().GetAllEmployees();
        Employees.DataBind();
    }

    protected void CreateEmployee_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/employees/create");
    }

    protected void Employees_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("/admin/employees/edit?id=" + e.CommandArgument.ToString());
        }

        if (e.CommandName == "Delete")
        {
            LoadEmployees();
        }
    }

    public bool YesOrNo(string value)
    {
        return value == "1";
    }

    public string Active(string active)
    {
        return active == "1" ? "<i class='bx bx-check-circle' style='color: #3ee23e;'></i>" : "<i class='bx bx-x-circle' style='color: red;'></i>";
    }

    public string GetName(string fullName, string nickName)
    {
        if (string.IsNullOrEmpty(nickName))
            return fullName;
        return fullName + " (" + nickName + ")";
    }
}