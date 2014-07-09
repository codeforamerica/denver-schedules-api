using System;
using System.Data.Common;
using System.Configuration;

namespace Schedules.API.Tasks
{
  public static class Db
  {
    private const string connectionName = "geospatial";

    public static DbConnection Connect() {
      // Reference: github.com/gregoryjscott/chic/TaskExtensions
      var connectionString = ConfigurationManager.ConnectionStrings[connectionName];
      var factory = DbProviderFactories.GetFactory (connectionString.ProviderName);

      if(factory == null)
        throw new Exception("Could not obtain factory for provider \""+connectionString.ProviderName+"\""); 

      DbConnection connection = factory.CreateConnection();
      if (connection == null)
        throw new Exception("Could not obtain connection from factory for " + connectionString.ProviderName);

      connection.ConnectionString = connectionString.ConnectionString;
      connection.Open ();

      return connection;
    }
  }
}

