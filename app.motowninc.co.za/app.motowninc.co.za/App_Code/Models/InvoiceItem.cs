public class InvoiceItem
{
    public string InvoiceItemId { get; set; }
    public string InvoiceId { get; set; }
    public string ProductId { get; set; }
    public string Code { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}