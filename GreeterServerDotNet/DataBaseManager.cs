using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeterServerDotNet
{
  class DataBaseManager
  {
    public DataBaseManager() { }
    private string connectionString
    {
      get { return "Data Source=193.225.33.71;User Id=SZG6RP;Password=szelektcsillag;"; }
    }
    protected OracleConnection getConnection()
    {
      OracleConnection connection = new OracleConnection(connectionString);
      connection.Open();
      return connection;
    }

    public List<User> Select()
    {
      List<User> list = new List<User>();

      OracleCommand command = new OracleCommand();
      command.CommandType = System.Data.CommandType.Text;
      command.CommandText = "SELECT * FROM jatekosok order by clicks desc";
      command.Connection = getConnection();

      OracleDataReader reader = command.ExecuteReader();
      while (reader.Read())
      {
        User jatekos = new User();
        jatekos.Username = reader["username"].ToString();
        jatekos.Password = reader["password"].ToString();
        jatekos.Clicks = int.Parse(reader["clicks"].ToString());
        list.Add(jatekos);
      }
      command.Connection.Dispose();
      command.Connection.Close();
      return list;
    }

    public bool Add(string username, string password)
    {
      OracleConnection connection = getConnection();

      OracleCommand command = new OracleCommand();
      command.CommandType = System.Data.CommandType.StoredProcedure;
      command.CommandText = "sp_ins_user";
      command.Connection = connection;

      OracleParameter parameter_name = new OracleParameter();
      parameter_name.ParameterName = "p_username";
      parameter_name.DbType = System.Data.DbType.String;
      parameter_name.Value = username;
      command.Parameters.Add(parameter_name);

      OracleParameter p_password = new OracleParameter();
      p_password.ParameterName = ":p_password";
      p_password.DbType = System.Data.DbType.String;
      p_password.Value = password;
      command.Parameters.Add(p_password);

      try
      {
        return command.ExecuteNonQuery() == -1;
      }
      catch
      {
        Console.WriteLine("dublicate");
        return false;
      }

    }

    public bool Update(string username)
    {
      OracleCommand command = new OracleCommand();
      command.CommandType = System.Data.CommandType.StoredProcedure;
      command.CommandText = "sp_upd_user";

      OracleParameter parameter_name = new OracleParameter();
      parameter_name.ParameterName = "p_username";
      parameter_name.DbType = System.Data.DbType.String;
      parameter_name.Value = username;
      command.Parameters.Add(parameter_name);

      command.Connection = getConnection();
      return command.ExecuteNonQuery() == -1;
    }

    public bool Reset(string username)
    {
      OracleCommand command = new OracleCommand();
      command.CommandType = System.Data.CommandType.StoredProcedure;
      command.CommandText = "sp_reset_user";

      OracleParameter parameter_name = new OracleParameter();
      parameter_name.ParameterName = "p_username";
      parameter_name.DbType = System.Data.DbType.String;
      parameter_name.Value = username;
      command.Parameters.Add(parameter_name);

      command.Connection = getConnection();
      return command.ExecuteNonQuery() == -1;
    }

    public string ClickBetolt(string username)
    {
      OracleConnection connection = getConnection();

      OracleCommand command = new OracleCommand();

      command.CommandType = System.Data.CommandType.StoredProcedure;
      command.CommandText = "sp_getclicks_user";
      command.Connection = connection;

      command.Parameters.Add("p_count", OracleDbType.Int32);
      command.Parameters["p_count"].Direction = System.Data.ParameterDirection.ReturnValue;

      OracleParameter parameter_name = new OracleParameter();
      parameter_name.ParameterName = "p_username";
      parameter_name.DbType = System.Data.DbType.String;
      parameter_name.Value = username;
      command.Parameters.Add(parameter_name);

      try
      {
        command.ExecuteNonQuery();
        return command.Parameters[0].Value.ToString();
      }
      catch
      {
        return "";
      }

    }

    public bool Login(string username, string password)
    {
      OracleConnection connection = getConnection();

      OracleCommand command = new OracleCommand();

      command.CommandType = System.Data.CommandType.StoredProcedure;
      command.CommandText = "sp_login_user";
      command.Connection = connection;

      command.Parameters.Add("p_count", OracleDbType.Int32);
      command.Parameters["p_count"].Direction = System.Data.ParameterDirection.ReturnValue;

      OracleParameter parameter_name = new OracleParameter();
      parameter_name.ParameterName = "p_username";
      parameter_name.DbType = System.Data.DbType.String;
      parameter_name.Value = username;
      command.Parameters.Add(parameter_name);

      OracleParameter p_password = new OracleParameter();
      p_password.ParameterName = ":p_password";
      p_password.DbType = System.Data.DbType.String;
      p_password.Value = password;
      command.Parameters.Add(p_password);
      

      try
      {
        command.ExecuteNonQuery();
        return int.Parse(command.Parameters[0].Value.ToString()) == 1;
      }
      catch
      {
        return false;
      }

    }

    public bool Register(string username, string password)
    {
      OracleConnection connection = getConnection();

      OracleCommand command = new OracleCommand();

      command.CommandType = System.Data.CommandType.StoredProcedure;
      command.CommandText = "sp_register_user";
      command.Connection = connection;

      command.Parameters.Add("p_count", OracleDbType.Int32);
      command.Parameters["p_count"].Direction = System.Data.ParameterDirection.ReturnValue;

      OracleParameter parameter_name = new OracleParameter();
      parameter_name.ParameterName = "p_username";
      parameter_name.DbType = System.Data.DbType.String;
      parameter_name.Value = username;
      command.Parameters.Add(parameter_name);

      OracleParameter p_password = new OracleParameter();
      p_password.ParameterName = ":p_password";
      p_password.DbType = System.Data.DbType.String;
      p_password.Value = password;
      command.Parameters.Add(p_password);
      try
      {
        command.ExecuteNonQuery();
        return int.Parse(command.Parameters[0].Value.ToString()) == 1;
      }
      catch
      {
        return true;
      }

    }
  }
}
