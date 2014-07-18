using System;
using System.Data.Common;
using System.Configuration;

namespace Schedules.API.Tasks
{
  public static class Db
  {
    private const string providerKey = "GEOSPATIAL-PROVIDER-NAME";
    private const string connectionKey = "GEOSPATIAL-CONNECTION-STRING";


    public static DbConnection Connect() {
      // Reference: github.com/gregoryjscott/chic/TaskExtensions
      var providerName = System.Environment.GetEnvironmentVariable (providerKey);
      var connectionString = System.Environment.GetEnvironmentVariable (connectionKey);
      var factory = DbProviderFactories.GetFactory (providerName);

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

