using System;
using System.Data.Common;
using Schedules.API.Helpers;

namespace Schedules.API.Tasks
{
  public static class Db
  {
    private const string providerKey = "GEOSPATIAL_PROVIDER_NAME";
    private const string connectionKey = "GEOSPATIAL_CONNECTION_STRING";


    public static DbConnection Connect() {
      // Reference: github.com/gregoryjscott/chic/TaskExtensions
      //var providerName = EnvironmentVariableHelper.GetEnvironmentVariable(providerKey);
      //var connectionString = EnvironmentVariableHelper.GetEnvironmentVariable(connectionKey);

     
      var providerName = "Npgsql";
      var connectionString = "Server=localhost;UserId=yunhao;Password=yunhao;Port=5432;Database=denver_schedules_development"; 

      DbProviderFactory factory;
      try{
        factory = DbProviderFactories.GetFactory (providerName);
      }
      catch(Exception) { // TODO: Figure out how to load config for tests (Shim for db tests)
        factory = Npgsql.NpgsqlFactory.Instance;
      }

      if(factory == null)
        throw new Exception("Could not obtain factory for provider \""+providerName+"\""); 

      DbConnection connection = factory.CreateConnection();
      if (connection == null)
        throw new Exception("Could not obtain connection from factory for " + providerName);

      connection.ConnectionString = connectionString;
      connection.Open ();

      return connection;
    }
  }
}

