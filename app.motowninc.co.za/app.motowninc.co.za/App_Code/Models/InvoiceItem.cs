public class InvoiceItem
{
    public string InvoiceItemId { get; set; }
    public string InvoiceId { get; set; }
    public string PileDiameter { get; set; }
    public decimal NumberOfPiles { get; set; }
    public decimal PileDepths { get; set; }
    public decimal SumPileLength { get; set; }
    public decimal WeightPerCage { get; set; }
    public decimal TotalWeight { get; set; }
    public decimal FillOnSite { get; set; }
    public decimal Concrete { get; set; }
    public decimal Wastage { get; set; }
    public decimal TotalConcreteGrout { get; set; }
    public decimal Rebar { get; set; }
    public decimal Day { get; set; }
    public decimal Duration { get; set; }
    public decimal Litres { get; set; }
    public decimal AllowedLitres { get; set; }
    public decimal DrillRateProfitMeter { get; set; }
}