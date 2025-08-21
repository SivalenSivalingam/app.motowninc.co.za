using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

public class Reports
{
    public DataTable Invoices(DateTime fromDate, DateTime toDate)
    {
        StringBuilder sql = new StringBuilder("SELECT * ");
        sql.Append("FROM Invoices ");
        sql.Append("WHERE DATE(Invoices.DateCreated) BETWEEN DATE(@FromDate) AND DATE(@ToDate) ");
        sql.Append("ORDER BY Invoices.DateCreated DESC;");

        return new DatabaseTable().Select(sql.ToString(),
          new List<MySqlParameter> {
                new MySqlParameter() { MySqlDbType = MySqlDbType.Date, ParameterName = "@FromDate", Value = fromDate},
                new MySqlParameter() { MySqlDbType = MySqlDbType.Date, ParameterName = "@ToDate", Value = toDate},
          });
    }
}