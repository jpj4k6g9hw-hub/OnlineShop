using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public static class DBHelper
{
    private static string GetConnString()
    {
        return ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
    }

    public static DataTable ExecuteDataTable(string sql, params MySqlParameter[] parameters)
    {
        using (var conn = new MySqlConnection(GetConnString()))
        using (var cmd = new MySqlCommand(sql, conn))
        using (var da = new MySqlDataAdapter(cmd))
        {
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    public static int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
    {
        using (var conn = new MySqlConnection(GetConnString()))
        using (var cmd = new MySqlCommand(sql, conn))
        {
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }

    public static object ExecuteScalar(string sql, params MySqlParameter[] parameters)
    {
        using (var conn = new MySqlConnection(GetConnString()))
        using (var cmd = new MySqlCommand(sql, conn))
        {
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            conn.Open();
            return cmd.ExecuteScalar();
        }
    }
}