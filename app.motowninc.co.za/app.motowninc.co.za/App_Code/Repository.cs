using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

public class Repository
{
    public string GetMySqlConnection()
    {
        return "server=" + ConfigurationManager.AppSettings["Server"] + ";uid=" + ConfigurationManager.AppSettings["UID"] + ";pwd=" + ConfigurationManager.AppSettings["PWD"] + ";database=" + ConfigurationManager.AppSettings["Database"] + ";";
    }

    public DataTable Query(string sql, List<MySqlParameter> parameters = null)
    {
        var dataTable = new DataTable();

        using (MySqlConnection connection = new MySqlConnection(GetMySqlConnection()))
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                if (parameters != null)
                {
                    foreach (MySqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                var sqlDataAdapter = new MySqlDataAdapter(command);

                sqlDataAdapter.Fill(dataTable);
            }

            connection.Close();
        }

        GC.Collect();

        return dataTable;
    }

    public DataSet Queries(string sql, List<MySqlParameter> parameters = null)
    {
        var dataSet = new DataSet();

        using (MySqlConnection connection = new MySqlConnection(GetMySqlConnection()))
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                if (parameters != null)
                {
                    foreach (MySqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                var sqlDataAdapter = new MySqlDataAdapter(command);

                sqlDataAdapter.Fill(dataSet);
            }

            connection.Close();
        }

        GC.Collect();

        return dataSet;
    }

    public void Command(string sql, List<MySqlParameter> parameters = null)
    {
        using (MySqlConnection connection = new MySqlConnection(GetMySqlConnection()))
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                if (parameters != null)
                {
                    foreach (MySqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}