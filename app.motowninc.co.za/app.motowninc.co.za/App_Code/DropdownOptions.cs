using System.Collections.Generic;

public class DropdownOptions
{
    public List<string> ProductTypes = new List<string>
        {
            "Audio",
            "Battery",
            "Rims",
            "Tyres",
        };

    public List<string> CustomerAccountTypes = new List<string>
        {
            "Cash",
            "Account",
            "Finance",
        };

    public List<string> EmployeeAccountTypes = new List<string>
        {
            "Admin",
            "POS",
        };

    public List<string> PaymentTypes = new List<string>
        {
            "Cash",
            "Card",
            "Account",
            "Finance",
            "Split",
        };
}