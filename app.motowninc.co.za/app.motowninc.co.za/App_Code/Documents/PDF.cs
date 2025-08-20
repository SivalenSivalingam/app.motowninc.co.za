using MigraDoc.DocumentObjectModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

public class PDF
{
    public Tuple<Document, string> CreateInvoice(string invoiceId)
    {
        PDFHelper pdfHelper = new PDFHelper();

        var dataSet = GetData(invoiceId);

        var invoices = dataSet.Tables[0].Rows[0];

        var invoiceItems = dataSet.Tables[1];

        var settings = dataSet.Tables[2].Rows[0];

        var document = pdfHelper.AddDocument();

        var section = pdfHelper.AddSection(document);

        pdfHelper.AddPageBanner(section);

        var columnWidth = new List<string>() { "9.75cm", "9.75cm" };

        pdfHelper.AddLeftRightColumns(
                pdfHelper.AddTable(section, columnWidth, 0, 10),
                "Customer Name : " + invoices["FullName"].ToString(),
                "Invoice No# : " + invoices["InvoiceId"].ToString().Substring(0, 8).ToUpper());

        pdfHelper.AddLeftRightColumns(
                pdfHelper.AddTable(section, columnWidth, 0, 10),
                "Contact Number : " + invoices["ContactNumber"].ToString(),
                "Invoice Date : " + DateTime.Parse(invoices["DateCreated"].ToString()).Date.ToString("MM/dd/yyyy"));

        var vehicleInformation = "";
        if (!string.IsNullOrEmpty(invoices["Make"].ToString()))
        {
            vehicleInformation = "Vehicle : " + invoices["Make"].ToString() + " - " + invoices["Model"].ToString() +" - " + invoices["LicensePlate"].ToString() + " (" + invoices["Mileage"].ToString() + "KM)";
        }

        pdfHelper.AddLeftRightColumns(
                pdfHelper.AddTable(section, columnWidth, 0, 10),
                vehicleInformation,
                "Payment Type : " + invoices["PaymentType"].ToString());

        // Invoice Items

        columnWidth = new List<string>() { "2cm", "7.50cm", "2cm", "3cm", "2cm", "3cm" };

        pdfHelper.Add6ColumnHeaders(
                pdfHelper.AddTableWithFont(section, columnWidth, 1, 10),
                "Code",
                "Item",
                "Quantity",
                "Unti Price",
                "Discount",
                "Total Price");

        decimal subtotal = 0;
        decimal discount = 0;

        foreach (DataRow item in invoiceItems.Rows)
        {
            pdfHelper.Add6Columns(
                pdfHelper.AddTableWithFont(section, columnWidth, 1, 10),
                item["Code"].ToString(),
                item["Name"].ToString(),
                item["Quantity"].ToString(),
                "R" + item["Price"].ToString(),
                "R" + item["Discount"].ToString(),
                "R" + pdfHelper.CalculateTotalUnitPrice(item["Quantity"].ToString(), item["Price"].ToString(), item["Discount"].ToString()).ToString());

            subtotal += pdfHelper.GetDecimal(item["Quantity"].ToString()) * pdfHelper.GetDecimal(item["Price"].ToString());
            discount += pdfHelper.GetDecimal(item["Discount"].ToString());
        }

        pdfHelper.AddSpace(section, 5);

        // Invoice Total

        var table = pdfHelper.AddTable(section, new List<string>() { "16.50cm", "3cm" });

        pdfHelper.Add2Columns(table, "Subtotal :", "R" + Math.Round(subtotal, 2).ToString(), 8);
        pdfHelper.Add2Columns(table, "Discount :", "R" + Math.Round(discount, 2).ToString(), 8);

        subtotal = subtotal - discount;

        decimal vat = subtotal * pdfHelper.GetDecimal("0.15");
        pdfHelper.Add2Columns(table, "VAT :", "R" + Math.Round(vat, 2).ToString(), 8);
        pdfHelper.Add2Columns(table, "Total :", "R" + Math.Round(double.Parse(invoices["Total"].ToString()), 2).ToString(), 8);

        if (invoices["PaymentType"].ToString().Trim() == "Cash")
        {
            pdfHelper.Add2Columns(table, "Cash Received :", "R" + Math.Round(double.Parse(invoices["CashReceived"].ToString()), 2).ToString(), 8);

            var cashReturned = double.Parse(invoices["CashReceived"].ToString()) - double.Parse(invoices["Total"].ToString());

            pdfHelper.Add2Columns(table, "Cash Returned :", "R" + Math.Round(cashReturned, 2).ToString(), 8);
        }

        // Notes

        string notes = invoices["Note"].ToString();

        if (notes.Length > 0)
        {
            pdfHelper.AddSpace(section, 5);

            pdfHelper.AddHeaderText(section, "Notes", 10, true, ParagraphAlignment.Left);
            pdfHelper.AddHeaderText(section, notes, 8, false, ParagraphAlignment.Left);
        }

        // Invoice Terms & Conditions

        string termsAndConditions = settings["TermsAndConditions"].ToString();

        if (termsAndConditions.Length > 0)
        {
            pdfHelper.AddSpace(section, 5);
            pdfHelper.AddHeaderText(section, "Terms & Conditions", 10, true, ParagraphAlignment.Left);
            pdfHelper.AddHeaderText(section, termsAndConditions, 8, false, ParagraphAlignment.Left);
        }

        // Footer

        pdfHelper.Footer(document, section);

        return new Tuple<Document, string>(document, "Invoice-" + invoiceId.Substring(0, 8).ToUpper());
    }

    public DataSet GetData(string invoiceId)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("SELECT * FROM Invoices WHERE InvoiceId = @invoiceId;");
        sql.Append("SELECT * FROM InvoiceItems WHERE InvoiceId = @invoiceId;");
        sql.Append("SELECT * FROM Settings WHERE SettingId = 1;");

        return new Repository().Queries(sql.ToString(), new List<MySqlParameter> { new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@invoiceId", Value = invoiceId } });
    }
}