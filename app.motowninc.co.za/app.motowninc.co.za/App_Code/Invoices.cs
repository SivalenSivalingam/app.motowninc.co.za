using System.Data;

public class Invoices
{
    public DataTable GetAllInvoices()
    {
        return new DatabaseTable().Select("SELECT Invoices.InvoiceId, IF(LENGTH(CompanyName)>0, CompanyName, FullName) AS Customer, AreaCosts.Location AS 'AreaCostLocation', Invoices.Location FROM Invoices INNER JOIN Customers ON Customers.CustomerId = Invoices.CustomerId INNER JOIN AreaCosts ON AreaCosts.AreaCostId = Invoices.AreaCostId WHERE Invoices.Type = 'Invoice' AND Deleted = 0;");
    }

    public DataTable GetAllQuotes()
    {
        return new DatabaseTable().Select("SELECT Invoices.InvoiceId, IF(LENGTH(CompanyName)>0, CompanyName, FullName) AS Customer, AreaCosts.Location AS 'AreaCostLocation', Invoices.Location FROM Invoices INNER JOIN Customers ON Customers.CustomerId = Invoices.CustomerId INNER JOIN AreaCosts ON AreaCosts.AreaCostId = Invoices.AreaCostId WHERE Invoices.Type = 'Quote' AND Deleted = 0;");
    }
}