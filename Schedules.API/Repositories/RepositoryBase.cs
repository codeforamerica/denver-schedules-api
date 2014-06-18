using System;
using System.Configuration;
using System.Data.Common;
using Npgsql;

namespace Schedules.API.Repositories
{
  public class RepositoryBase: IDisposable
  {
    private const string connectionName = "geospatial";
    public DbConnection connection;

    public RepositoryBase ()
    {
      connection = GetConnection ();
    }

    private DbConnection GetConnection()
    {// Reference: http://stackoverflow.com/questions/1046042/asp-net-how-to-create-a-connection-from-a-web-config-connectionstring
      var connectionString = ConfigurationManager.ConnectionStrings[connectionName];
      var factory = DbProviderFactories.GetFactory (connectionString.ProviderName);

      if(factory == null)
        throw new Exception("Could not obtain factory for provider \""+connectionString.ProviderName+"\""); 

      DbConnection connection = factory.CreateConnection();
      if (connection == null)
        throw new Exception("Could not obtain connection from factory for " + connectionString.ProviderName);

      connection.ConnectionString = connectionString.ConnectionString;

      return connection;
    }

    public void Dispose()
    {
      if (connection != null)
        connection.Dispose ();
    }
  }
}

