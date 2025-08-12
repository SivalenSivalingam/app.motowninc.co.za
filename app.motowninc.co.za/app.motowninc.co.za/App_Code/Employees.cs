using System.Data;


public class Employees
{
    public DataTable GetAllEmployees()
    {
        return new DatabaseTable().Select("SELECT * FROM Employees WHERE AccountType != 'Admin' ORDER BY Fullname;");
    }
}