using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;


public class Cart
{
    public DataTable Get(string employeeId)
    {
        return new Repository().Query("SELECT * FROM Cart WHERE EmployeeId = @EmployeeId", new List<MySqlParameter> { new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@EmployeeId", Value = employeeId } });
    }

    private DataTable Get(string employeeId, string productId)
    {
        return new Repository().Query("SELECT * FROM Cart WHERE ProductId = @ProductId AND ProductId = @ProductId", new List<MySqlParameter> {
            new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@EmployeeId", Value = employeeId },
            new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@ProductId", Value = productId},
        });
    }

    public void AddToCart(string employeeId, string productId, int quantity)
    {
        if (Get(employeeId, productId).Rows.Count == 0)
        {
            new Repository().Command("INSERT INTO Cart (EmployeeId, ProductId, Quantity) VALUE (@EmployeeId, @ProductId, @Quantity)",
                new List<MySqlParameter>
                    {
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@EmployeeId", Value = employeeId },
                        new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@ProductId", Value = productId},
                        new MySqlParameter() { MySqlDbType = MySqlDbType.Int32, ParameterName = "@Quantity", Value = quantity},
                    });
        }
        else
        {
            UpdateCartQuantity(employeeId, productId, quantity);
        }
    }

    private void UpdateCartQuantity(string employeeId, string productId, int quantity)
    {
        new Repository().Command("UPDATE Cart SET Quantity = Quantity + @Quantity WHERE ProductId = @ProductId AND EmployeeId = @EmployeeId",
        new List<MySqlParameter> {
                new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@EmployeeId", Value = employeeId },
                new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@ProductId", Value = productId},
                new MySqlParameter() { MySqlDbType = MySqlDbType.Int32, ParameterName = "@Quantity", Value = quantity},
            });
    }

    public void Delete(string employeeId, string ProductId)
    {
        new Repository().Command("DELETE FROM Cart WHERE ProductId = @ProductId AND EmployeeId = @EmployeeId LIMIT 1", new List<MySqlParameter> {
            new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@EmployeeId", Value = employeeId },
                new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@ProductId", Value = ProductId},
        });
    }

    public void OverrideCartQuantity(string employeeId, string productId, int quantity)
    {
        new Repository().Command("UPDATE Cart SET Quantity = @Quantity WHERE ProductId = @ProductId AND EmployeeId = @EmployeeId",
        new List<MySqlParameter> {
                new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@EmployeeId", Value = employeeId },
                new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@ProductId", Value = productId},
                new MySqlParameter() { MySqlDbType = MySqlDbType.Int32, ParameterName = "@Quantity", Value = quantity},
            });
    }
}