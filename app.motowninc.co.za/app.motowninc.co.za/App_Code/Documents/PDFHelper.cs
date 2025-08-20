using System.Collections.Generic;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System.Web;
using Image = MigraDoc.DocumentObjectModel.Shapes.Image;
using System;
using System.Globalization;
using System.Configuration;

public class PDFHelper
{
    public Document AddDocument()
    {
        var document = new Document();
        document.Info.Title = "Software24";
        return document;
    }

    public Section AddSection(Document document)
    {
        var section = document.AddSection();
        section.PageSetup.StartingNumber = 1;
        section.PageSetup.TopMargin = "0.5cm";
        section.PageSetup.LeftMargin = "0.5cm";

        return section;
    }

    public void AddPageBanner(Section section)
    {
        Image image = new Image(HttpContext.Current.Server.MapPath(("~/img/invoice-logo.png")))
        {
            Height = "4cm",
            Width = "18.50cm"
        };

        var table = AddTable(section, new List<string>() { "13.50cm", "5cm" });

        var row = table.AddRow();
        row.Cells[0].Add(image);
        row.Height = "4cm";
        row.VerticalAlignment = VerticalAlignment.Center;
    }

    public Table AddTable(Section section, List<string> columnWidth, int bordersWidth = 0, int leftIndent = 0, bool tableRowGrouped = false)
    {
        Table table = section.AddTable();
        table.Rows.LeftIndent = leftIndent;

        if (tableRowGrouped)
        {
            table.Borders.Left.Width = bordersWidth;
            table.Borders.Bottom.Width = bordersWidth;
            table.Borders.Right.Width = bordersWidth;
        }
        else
        {
            table.Borders.Width = bordersWidth;
        }

        foreach (string width in columnWidth)
        {
            table.AddColumn(width);
        }

        return table;
    }

    public Table AddTableWithFont(Section section, List<string> columnWidth, int bordersWidth = 0, int leftIndent = 0, bool tableRowGrouped = false)
    {
        Table table = section.AddTable();
        table.Rows.LeftIndent = leftIndent;
        table.Format.Font.Size = 7;

        if (tableRowGrouped)
        {
            table.Borders.Left.Width = bordersWidth;
            table.Borders.Bottom.Width = bordersWidth;
            table.Borders.Right.Width = bordersWidth;
        }
        else
        {
            table.Borders.Width = bordersWidth;
        }

        foreach (string width in columnWidth)
        {
            table.AddColumn(width);
        }

        return table;
    }

    public void AddHeaderText(Section section, string heading, int fontSize, bool setAsBold = false, ParagraphAlignment paragraphAlignment = ParagraphAlignment.Center)
    {
        Table table = section.AddTable();
        table.Rows.LeftIndent = 0;
        table.Borders.Width = 0;

        table.AddColumn("18.50cm");

        var row = table.AddRow();
        row.Cells[0].AddParagraph(heading);
        row.Cells[0].Format.Font.Size = fontSize;
        row.Height = 15;
        row.VerticalAlignment = VerticalAlignment.Center;
        row.Format.Alignment = paragraphAlignment;
        row.Format.Font.Bold = setAsBold;
    }

    public void AddSpace(Section section, int height = 20)
    {
        Table table = section.AddTable();
        table.Rows.LeftIndent = 0;
        table.Borders.Width = 0;

        table.AddColumn("19.50cm");

        var row = table.AddRow();
        row.Cells[0].AddParagraph("");
        row.Height = height;
    }

    public void Add6ColumnHeaders(Table table, string column1, string column2, string column3, string column4, string column5, string column6)
    {
        var row = table.AddRow();
        row.Height = 20;
        row.VerticalAlignment = VerticalAlignment.Center;
        row.Format.Font.Color = Colors.White;

        row.Cells[0].AddParagraph(column1);
        row.Cells[0].Shading.Color = Colors.Black;

        row.Cells[1].AddParagraph(column2);
        row.Cells[1].Shading.Color = Colors.Black;

        row.Cells[2].AddParagraph(column3);
        row.Cells[2].Shading.Color = Colors.Black;

        row.Cells[3].AddParagraph(column4);
        row.Cells[3].Shading.Color = Colors.Black;

        row.Cells[4].AddParagraph(column5);
        row.Cells[4].Shading.Color = Colors.Black;

        row.Cells[5].AddParagraph(column6);
        row.Cells[5].Shading.Color = Colors.Black;

    }

    public void Add6Columns(Table table, string column1, string column2, string column3, string column4, string column5, string column6)
    {
        var row = table.AddRow();
        row.Height = 25;
        row.TopPadding = 10;
        row.BottomPadding = 10;
        row.VerticalAlignment = VerticalAlignment.Top;
        table.Borders.Top.Width = 0;
        table.Borders.Left.Width = 0;
        table.Borders.Right.Width = 0;

        row.Cells[0].AddParagraph(column1);
        row.Cells[1].AddParagraph(column2);
        row.Cells[2].AddParagraph(column3);
        row.Cells[3].AddParagraph(column4);
        row.Cells[4].AddParagraph(column5);
        row.Cells[5].AddParagraph(column6);
    }

    public void Add2Columns(Table table, string column1, string column2, int fontSize = 12)
    {
        var row = table.AddRow();
        row.Height = 15;
        row.VerticalAlignment = VerticalAlignment.Top;
        row.Format.Font.Bold = true;

        row.Cells[0].AddParagraph(column1);
        row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        row.Cells[0].Format.Font.Size = fontSize;

        row.Cells[1].AddParagraph(column2);
        row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
        row.Cells[1].Format.Font.Size = fontSize;
    }

    public void AddLeftRightColumns(Table table, string column1, string column2)
    {
        var row = table.AddRow();
        row.Height = 20;
        row.VerticalAlignment = VerticalAlignment.Top;
        row.Format.Font.Bold = true;

        row.Cells[0].AddParagraph(column1);
        row.Cells[0].Format.Alignment = ParagraphAlignment.Left;

        row.Cells[1].AddParagraph(column2);
        row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
    }

    public decimal CalculateTotalUnitPrice(string Quantity, string Price, string Discount)
    {
        var newPrice = GetDecimal(Price) - GetDecimal(Discount);
        return GetDecimal(Quantity) * GetDecimal(newPrice.ToString());
    }

    public decimal GetDecimal(string value)
    {
        if (string.IsNullOrEmpty(value))
            return 0;

        return decimal.Parse(value.Trim().ToString().Replace(",", "."), CultureInfo.InvariantCulture);
    }

    public void Footer(Document document, Section section)
    {
        var footerTable = section.Footers.Primary.AddTable();
        footerTable.AddColumn("4.50cm");
        footerTable.AddColumn("10.50cm");
        footerTable.AddColumn("4.50cm");

        var rowFooter = footerTable.AddRow();
        rowFooter.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        rowFooter.Cells[0].AddParagraph(DateTime.Now.ToString("dd/MM/yyyy"));
        rowFooter.Cells[1].Format.Alignment = ParagraphAlignment.Center;
        rowFooter.Cells[1].AddParagraph(ConfigurationManager.AppSettings["FooterCompanyName"] + " | " + ConfigurationManager.AppSettings["FooterCompanyContactNumber"] + " | " + ConfigurationManager.AppSettings["FooterCompanyEmailAddress"]);
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
}