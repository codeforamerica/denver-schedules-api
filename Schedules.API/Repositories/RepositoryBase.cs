using System;
using System.Configuration;
using System.Data.Common;
using Npgsql;

namespace Schedules.API.Repositories
{
  public class RepositoryBase: IDisposable
  {
    private const string providerKey = "GEOSPATIAL-PROVIDER-NAME";
    private const string connectionKey = "GEOSPATIAL-CONNECTION-STRING";
    public DbConnection connection;

    public RepositoryBase ()
    {
      connection = GetConnection ();
    }

    private DbConnection GetConnection()
    {// Reference: http://stackoverflow.com/questions/1046042/asp-net-how-to-create-a-connection-from-a-web-config-connectionstring
      var providerName = System.Environment.GetEnvironmentVariable (providerKey);
      var connectionString = System.Environment.GetEnvironmentVariable (connectionKey);
      var factory = DbProviderFactories.GetFactory (providerName);

      if(factory == null)
        throw new Exception("Could not obtain factory for provider \""+providerName+"\""); 

      connection = factory.CreateConnection();
      if (connection == null)
        throw new Exception("Could not obtain connection from factory for " + providerName);

      connection.ConnectionString = connectionString;
      connection.Open ();

      return connection;
    }

    public void Dispose()
    {
      if (connection != null)
        connection.Dispose ();
    }
  }
}

