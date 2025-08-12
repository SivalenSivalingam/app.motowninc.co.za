using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

public partial class admin_settings_dashboard : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SelectedTab.Value = "#TermsAndConditions";
            LoadSettings();
        }
    }

    private void LoadSettings()
    {
        DataRow settings = new DatabaseTable().Select("SELECT * FROM Settings WHERE SettingId = '1';").Rows[0];

        TermsAndConditionsContent.InnerHtml = settings["TermsAndConditions"].ToString();
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        var result = new DatabaseTable().Update("Settings", new List<MySqlParameter> {
                         new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@SettingId", Value = "1"},
                         new MySqlParameter() { MySqlDbType = MySqlDbType.LongText, ParameterName="@TermsAndConditions", Value = TermsAndConditionsContent.InnerHtml}
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