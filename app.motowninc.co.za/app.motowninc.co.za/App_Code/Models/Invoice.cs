public class Invoice
{
    public string InvoiceId { get; set; }
    public string Type { get; set; }
    public string AreaCostId { get; set; }
    public string Location { get; set; }
    public string CustomerId { get; set; }
    public string EmployeeId { get; set; }
    public decimal DistanceFromYard { get; set; }
    public decimal NumberOfAllowedTripsTruck { get; set; }
    public decimal TotalTruckKM { get; set; }
    public decimal NumberOfAllowedTripsVan { get; set; }
    public decimal VanAllowable { get; set; }
    public decimal AllowedVisits { get; set; }
    public decimal AllowedKM { get; set; }
    public bool Deleted { get; set; }
}