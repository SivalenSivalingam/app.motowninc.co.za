using System;

public class Payslip
{
    public string PayslipId { get; set; }
    public string EmployeeId { get; set; }
    public string PayPeriodId { get; set; }
    public DateTime PayPeriod { get; set; }
    public decimal PayRate { get; set; }
    public decimal NormalTimeHours { get; set; }
    public decimal NormalTimeAmount { get; set; }
    public decimal OverTimeHours { get; set; }
    public decimal OverTimeAmount { get; set; }
    public decimal DoubleTimeHours { get; set; }
    public decimal DoubleTimeAmount { get; set; }
    public decimal TravellingTimeHours { get; set; }
    public decimal TravellingTimeAmount { get; set; }
    public decimal LOADays { get; set; }
    public decimal LOARate { get; set; }
    public decimal LOAAmount { get; set; }
    public decimal GrossPay { get; set; }
    public decimal UIF { get; set; }
    public decimal PAYE { get; set; }
    public decimal Deductions { get; set; }
    public decimal NettPay { get; set; }
}
