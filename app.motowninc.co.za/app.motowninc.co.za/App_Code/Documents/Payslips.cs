using System.Collections.Generic;
using System.Text;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MySql.Data.MySqlClient;
using System.Data;
using System;
using Image = MigraDoc.DocumentObjectModel.Shapes.Image;
using System.Web;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

public class Payslips
{
    private readonly string CompanyName = "Syndicate Piling (Pty) Ltd";
    private readonly string ContactNumber = "031 827 9669 / 084 734 9939";
    private readonly string EmailAddress = "admin@syndicatepiling.co.za";
    private readonly string Website = "www.syndicatepiling.co.za";
    private readonly string Address = "22 Kingfisher Park, 45 Marseilles Crescent, Briardene, 4051";
    private readonly string ShadingColorHexCode = "";
    private readonly string FontShadingColorHexCode = "";

    public Tuple<Document, string> Create(string payslipId)
    {
        DataSet dataSet = GetData(payslipId);

        DataRow payslip = dataSet.Tables[0].Rows[0];

        var document = new Document();
        document.Info.Title = "";
        document.DefaultPageSetup.Orientation = Orientation.Portrait;

        Section section = document.AddSection();
        section.PageSetup.StartingNumber = 1;
        section.PageSetup.TopMargin = "1cm";
        section.PageSetup.LeftMargin = "1cm";
        section.PageSetup.RightMargin = "1cm";

        //-------------------------------------------------------------------------------------------------------------------------------
        //Logo + Company Information
        //-------------------------------------------------------------------------------------------------------------------------------

        Table table = section.AddTable();
        table.Style = "Table1";
        table.Rows.LeftIndent = 0;
        table.Borders.Width = 0;

        // 18.50
        table.AddColumn("9.25cm");
        table.AddColumn("9.25cm");

        Paragraph paragraphImage = new Paragraph();

        Image image = new Image(HttpContext.Current.Server.MapPath(("~/img/payslip-logo.jpg")));
        image.Height = "3cm";
        image.Width = "8cm";
        paragraphImage.Add(image);

        Paragraph paragraphContent = new Paragraph();
        Font font = new Font
        {
            Size = 17,
            Bold = true
        };

        paragraphContent.AddFormattedText("Payslip", font);
        paragraphContent.AddLineBreak();
        paragraphContent.AddFormattedText("-", new Font { Size = 10, Bold = false, Color = Color.FromRgb(255, 255, 255) });
        paragraphContent.AddLineBreak();
        paragraphContent.AddFormattedText(CompanyName, new Font { Size = 13 });
        paragraphContent.AddLineBreak();
        font = new Font
        {
            Size = 10,
            Bold = false
        };
        paragraphContent.AddFormattedText(ContactNumber, font);
        paragraphContent.AddLineBreak();
        paragraphContent.AddFormattedText(EmailAddress, font);
        paragraphContent.AddLineBreak();
        paragraphContent.AddFormattedText(Website, font);
        paragraphContent.AddLineBreak();
        paragraphContent.AddFormattedText(Address, font);
        paragraphContent.AddLineBreak();

        AddCellText(table, paragraphImage, paragraphContent, 17, true);

        // Employee Info
        table = section.AddTable();
        table.Style = "Table1";
        table.Rows.LeftIndent = 0;
        table.Borders.Width = 0;

        // 18.50
        table.AddColumn("4cm");
        table.AddColumn("7.25cm");
        table.AddColumn("4cm");
        table.AddColumn("3.25cm");

        Row4Column(table, "", "", "", "");
        Row4Column(table, "Full Name", payslip["FullName"].ToString(), "Date Engaged", payslip["DateEngaged"].ToString());
        Row4Column(table, "Department", payslip["Department"].ToString(), "Pay Period", DateTime.Parse(payslip["PayPeriod"].ToString()).ToString("dd/MM/yyyy"));
        Row4Column(table, "Position", payslip["Position"].ToString(), "Pay Rate", payslip["PayRate"].ToString());
        Row4Column(table, "Employee Number", payslip["EmployeeNumber"].ToString(), "", "");

        //Earnings
        Table earnings = new Table();
        earnings.Style = "Table2";
        earnings.Rows.LeftIndent = 0;
        earnings.Borders.Width = 1;

        // 18.50
        earnings.AddColumn("5.525cm");
        earnings.AddColumn("3.525cm");

        Row2Column(earnings, "Earnings", "", true, ShadingColorHexCode, FontShadingColorHexCode);

        Row2Column(earnings, "Normal Time : Hours " + payslip["NormalTimeHours"].ToString(), payslip["NormalTimeAmount"].ToString());

        if(GetDecimal(payslip["OverTimeAmount"].ToString()) > 0)
            Row2Column(earnings, "Over Time : Hours " + payslip["OverTimeHours"].ToString(), payslip["OverTimeAmount"].ToString());

        if (GetDecimal(payslip["DoubleTimeHours"].ToString()) > 0)
            Row2Column(earnings, "Double Time : Hours " + payslip["DoubleTimeHours"].ToString(), payslip["DoubleTimeHours"].ToString());

        if (GetDecimal(payslip["TravellingTimeAmount"].ToString()) > 0)
            Row2Column(earnings, "Travelling Time : Hours " + payslip["TravellingTimeHours"].ToString(), payslip["TravellingTimeAmount"].ToString());

        if (GetDecimal(payslip["LOAAmount"].ToString()) > 0)
            Row2Column(earnings, "LOA : Days " + payslip["LOADays"].ToString(), payslip["LOAAmount"].ToString());

        section.AddParagraph();
        section.AddParagraph();

        //Deductions
        Table deductions = new Table();
        deductions.Style = "Table2";
        deductions.Rows.LeftIndent = 0;
        deductions.Borders.Width = 1;

        // 18.50
        deductions.AddColumn("4.525cm");
        deductions.AddColumn("4.525cm");

        Row2Column(deductions, "Deductions", "", true, ShadingColorHexCode, FontShadingColorHexCode);

        Row2Column(deductions, "UIF", payslip["UIF"].ToString());

        Row2Column(deductions, "PAYE", payslip["PAYE"].ToString());

        if (GetDecimal(payslip["Deductions"].ToString()) > 0)
            Row2Column(deductions, "Deductions", payslip["Deductions"].ToString());

        AddTable(section, earnings, deductions);

        //Totals
        table = section.AddTable();
        table.Style = "Table3";
        table.Rows.LeftIndent = 0;
        table.Borders.Width = 0;

        // 18.50
        table.AddColumn("18.50cm");

        AddCellText(table, "");
        AddCellText(table, "Total Gross Pay : R" + payslip["GrossPay"].ToString());
        var TotalDeductions = GetDecimal(payslip["UIF"].ToString()) + GetDecimal(payslip["PAYE"].ToString()) + GetDecimal(payslip["Deductions"].ToString());
        AddCellText(table, "Total Deductions : R" + TotalDeductions.ToString());
        AddCellText(table, "Nett Pay : R" + payslip["NettPay"].ToString());

        // Footer
        Footer(document, section, CompanyName);

        return new Tuple<Document, string>(document, "Payslip-" + payslip["EmployeeNumber"].ToString() + "-" + DateTime.Parse(payslip["PayPeriod"].ToString()).ToString("yyyy/MM/dd").Replace("/","-"));
    }

    private void AddCellText(Table table, string column1, bool setAsBold = false, ParagraphAlignment paragraphAlignment = ParagraphAlignment.Right, int fontSize = 11)
    {
        var row = table.AddRow();
        row.Cells[0].AddParagraph(column1);
        row.Cells[0].Format.Alignment = paragraphAlignment;

        row.Height = 20;
        row.VerticalAlignment = VerticalAlignment.Center;
        row.Format.Font.Bold = true;
        row.Format.Font.Size = fontSize;
    }

    private string GetValueOrEmpty(string value)
    {
        return GetDecimal(value) > 0 ? value : string.Empty;
    }

    private decimal GetDecimal(string value)
    {
        if (string.IsNullOrEmpty(value))
            return 0;

        return decimal.Parse(value.Trim().ToString().Replace(",", "."), CultureInfo.InvariantCulture);
    }

    private void Row2Column(Table table, string column1, string column2, bool setAsBold = false, string shadingColorHex = "#000000", string fontColorHex = "#FFFFFF")
    {
        if (string.IsNullOrEmpty(column1))
            return;

        column2 = GetValueOrEmpty(column2);

        var row = table.AddRow();
        row.TopPadding = 5;
        row.BottomPadding = 5;
        row.VerticalAlignment = VerticalAlignment.Top;
        table.Borders.Top.Width = 0;
        table.Borders.Left.Width = 0;
        table.Borders.Right.Width = 0;
        table.Borders.Bottom.Width = 0.5;

        row.Cells[0].AddParagraph(column1);

        row.Cells[1].AddParagraph(column2);
        row.Cells[1].Format.Alignment = ParagraphAlignment.Right;

        row.Height = 15;
        row.VerticalAlignment = VerticalAlignment.Center;
        row.Format.Font.Bold = setAsBold;

        if (setAsBold)
        {
            if (string.IsNullOrEmpty(shadingColorHex))
                shadingColorHex = "#000000";

            if (string.IsNullOrEmpty(fontColorHex))
                fontColorHex = "#FFFFFF";

            var shadingColor = Color.Parse(shadingColorHex.Replace("#", "0xff"));
            var fontColor = Color.Parse(fontColorHex.Replace("#", "0xff"));
            row.Borders.Top.Color = Color.Parse(shadingColorHex.Replace("#", "0xff"));
            row.Borders.Bottom.Color = Color.Parse(shadingColorHex.Replace("#", "0xff"));

            row.Cells[0].Shading.Color = shadingColor;
            row.Cells[1].Shading.Color = shadingColor;
            row.Format.Font.Color = fontColor;
        }
    }

    private void Row4Column(Table table, string column1, string column2, string column3, string column4, int fontSize = 10)
    {
        var row = table.AddRow();
        row.Cells[0].AddParagraph(column1);
        row.Cells[0].Format.Font.Bold = true;

        row.Cells[1].AddParagraph(column2);
        row.Cells[1].Format.Alignment = ParagraphAlignment.Left;

        row.Cells[2].AddParagraph(column3);
        row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[2].Format.Font.Bold = true;

        row.Cells[3].AddParagraph(column4);
        row.Cells[3].Format.Alignment = ParagraphAlignment.Left;

        row.Height = 15;
        row.VerticalAlignment = VerticalAlignment.Center;
    }

    private void Footer(Document document, Section section, string companyName)
    {
        var footerTable = section.Footers.Primary.AddTable();
        footerTable.AddColumn("5.0cm");
        footerTable.AddColumn("8.48cm");
        footerTable.AddColumn("5.0cm");

        var rowFooter = footerTable.AddRow();
        rowFooter.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        rowFooter.Cells[0].AddParagraph(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
        rowFooter.Cells[1].Format.Alignment = ParagraphAlignment.Center;
        rowFooter.Cells[1].AddParagraph(companyName);
        rowFooter.Cells[2].Format.Alignment = ParagraphAlignment.Right;

        rowFooter.Cells[0].Format.Font.Size = 7;
        rowFooter.Cells[1].Format.Font.Size = 7;
        rowFooter.Cells[2].Format.Font.Size = 7;

        var pager = new Paragraph();
        pager.Format.Alignment = ParagraphAlignment.Right;
        pager.Format.AddTabStop(Unit.FromMillimeter(173), TabAlignment.Right);
        pager.AddFormattedText("Page ");
        pager.AddPageField();
        pager.AddFormattedText(" of ");
        pager.AddNumPagesField();

        rowFooter.Cells[2].Add(pager);
    }

    private void AddCellText(Table table, Paragraph column1, Paragraph column2, int fontSize = 10, bool setAsBold = false)
    {
        var row = table.AddRow();
        row.Cells[0].Add(column1);
        row.Cells[0].Format.Alignment = ParagraphAlignment.Left;

        row.Cells[1].Add(column2);
        row.Cells[1].Format.Alignment = ParagraphAlignment.Right;

        row.Height = 20;
        row.VerticalAlignment = VerticalAlignment.Top;
        row.Format.Font.Bold = setAsBold;
        row.Format.Font.Size = fontSize;
    }

    private DataSet GetData(string payslipId)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("SELECT * FROM Payslips INNER JOIN Employees ON Payslips.EmployeeId = Employees.EmployeeId WHERE Payslips.PayslipId = @PayslipId;");

        return new DatabaseTable().SQL(sql.ToString(), new List<MySqlParameter> { new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@PayslipId", Value = payslipId } });
    }

    private void AddTable(Section section, Table tableLeft, Table tableRight)
    {
        var table = section.AddTable();
        table.AddColumn("9.25cm");
        table.AddColumn("9.25cm");

        var row = table.AddRow();
        row.Cells[0].Elements.Add(tableLeft);
        row.Cells[1].Elements.Add(tableRight);
    }
}