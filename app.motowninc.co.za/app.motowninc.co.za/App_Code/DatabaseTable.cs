using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

public class DatabaseTable
{
    public DataTable Select(string sql, List<MySqlParameter> mySqlParameters = null)
    {
        try
        {
            return new Repository().Query(sql, mySqlParameters);
        }
        catch (Exception exception)
        {
            ExceptionLog(sql, mySqlParameters, exception.Message, exception.StackTrace);
            return new DataTable();
        }
    }

    public DataSet SQL(string sql, List<MySqlParameter> mySqlParameters = null)
    {
        try
        {
            return new Repository().Queries(sql, mySqlParameters);
        }
        catch (Exception exception)
        {
            ExceptionLog(sql, mySqlParameters, exception.Message, exception.StackTrace);
            return new DataSet();
        }
    }

    public Tuple<bool, string> Insert(string tableName, List<MySqlParameter> mySqlParameters)
    {
        StringBuilder sql = new StringBuilder("INSERT INTO " + tableName + " ");

        try
        {
            List<string> columns = new List<string>();
            List<string> query = new List<string>();

            foreach (MySqlParameter column in mySqlParameters)
            {
                columns.Add(column.ParameterName.Replace("@", ""));
                query.Add(column.ParameterName);
            }

            sql.Append("(" + string.Join(",", columns) + ") VALUES (" + string.Join(",", query) + ")");

            new Repository().Command(sql.ToString(), mySqlParameters);

            return Tuple.Create(true, string.Empty);
        }
        catch (Exception exception)
        {
            ExceptionLog(sql.ToString(), mySqlParameters, exception.Message, exception.StackTrace);
            return Tuple.Create(false, exception.Message);
        }
    }

    public Tuple<bool, string> InsertWithPassword(string tableName, List<MySqlParameter> mySqlParameters)
    {
        StringBuilder sql = new StringBuilder("INSERT INTO " + tableName + " ");

        try
        {
            List<string> columns = new List<string>();
            List<string> query = new List<string>();

            foreach (MySqlParameter column in mySqlParameters)
            {
                columns.Add(column.ParameterName.Replace("@", ""));

                if (column.ParameterName.ToLower().Contains("password"))
                {
                    query.Add("SHA2(@password,512)");
                }
                else
                {
                    query.Add(column.ParameterName);
                }
            }

            sql.Append("(" + string.Join(",", columns) + ") VALUES (" + string.Join(",", query) + ")");

            new Repository().Command(sql.ToString(), mySqlParameters);

            return Tuple.Create(true, string.Empty);
        }
        catch (Exception exception)
        {
            return Tuple.Create(false, exception.Message);
        }
    }

    public Tuple<bool, string> Update(string tableName, List<MySqlParameter> mySqlParameters)
    {
        StringBuilder sql = new StringBuilder("UPDATE " + tableName + " SET ");

        try
        {
            string ColumnId = mySqlParameters[0].ParameterName;

            List<string> query = new List<string>();

            foreach (MySqlParameter column in mySqlParameters)
            {
                if (column.ParameterName != ColumnId)
                    query.Add(column.ParameterName.Replace("@", "") + " = " + column.ParameterName);
            }

            sql.Append(string.Join(",", query));

            sql.Append(" WHERE " + ColumnId.Replace("@", "") + " = " + ColumnId);

            new Repository().Command(sql.ToString(), mySqlParameters);

            return Tuple.Create(true, string.Empty);
        }
        catch (Exception exception)
        {
            ExceptionLog(sql.ToString(), mySqlParameters, exception.Message, exception.StackTrace);
            return Tuple.Create(false, exception.Message);
        }
    }

    public Tuple<bool, string> UpdatePassword(string tableName, string primaryKey, string primaryValue, string password)
    {
        var sql = "UPDATE " + tableName + " SET Password = SHA2(@Password,512) WHERE " + primaryKey + " = @" + primaryKey;
        var mySqlParameter = new List<MySqlParameter> {
            new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@" + primaryKey, Value = primaryValue },
            new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@Password", Value = password }
        };

        try
        {
            new Repository().Command(sql, mySqlParameter);
            return Tuple.Create(true, string.Empty);
        }
        catch (Exception exception)
        {
            return Tuple.Create(false, exception.Message);
        }
    }

    public Tuple<bool, string> Delete(string tableName, string primaryKey, string primaryValue)
    {
        var sql = "DELETE FROM " + tableName + " WHERE " + primaryKey + " = @" + primaryKey + " LIMIT 1";
        var mySqlParameter = new List<MySqlParameter> { new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@" + primaryKey, Value = primaryValue } };

        try
        {
            new Repository().Command(sql, mySqlParameter);
            return Tuple.Create(true, string.Empty);
        }
        catch (Exception exception)
        {
            ExceptionLog(sql, mySqlParameter, exception.Message, exception.StackTrace);
            return Tuple.Create(false, exception.Message);
        }
    }

    private void ExceptionLog(string sqlQuery, List<MySqlParameter> sqlParameters, string message, string stackTrace)
    {
        new Repository()
            .Command(
            "INSERT INTO ExceptionLogs (SQLQuery, SQLParameters, Message, StackTrace) VALUES (@SQLQuery, @SQLParameters, @Message, @StackTrace)",
            new List<MySqlParameter>{
            new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@SQLQuery", Value = sqlQuery },
            new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@SQLParameters", Value = ExtractRawData(sqlParameters) },
            new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@Message", Value = message },
            new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName = "@StackTrace", Value = stackTrace }});
    }

    private string ExtractRawData(List<MySqlParameter> mySqlParameter)
    {
        if (mySqlParameter == null)
            return null;

        StringBuilder rawData = new StringBuilder();

        foreach (MySqlParameter parameter in mySqlParameter)
        {
            if (parameter.Value != null && !string.IsNullOrEmpty(parameter.Value.ToString()))
            {
                rawData.Append(parameter.ParameterName.Replace("@", string.Empty) + " = " + parameter.Value);
                rawData.Append("<br/>");
            }
        }

        return rawData.ToString();
    }
}