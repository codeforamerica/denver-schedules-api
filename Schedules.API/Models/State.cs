using System;
using Schedules.API.Extensions;
using System.Collections.Generic;
using Schedules.API.Tasks;

namespace Schedules.API.Models
{
  /// <summary>
  /// Status check of the app
  /// </summary>
  public class State
  {
    public State ()
    {
      Status = CheckAppForErrors ();
      Updated = DateTime.Now.ToUnixTimestamp ();
    }

    /// <summary>
    /// May contain either "ok" or an error message
    /// </summary>
    /// <value>The status.</value>
    public String Status {
      get;
      set;
    }

    /// <summary>
    /// Unix timestamp indicating when the status check occurred by the app.
    /// </summary>
    /// <value>The updated.</value>
    public int Updated {
      get;
      set;
    }

    /// <summary>
    /// Third party services the app uses
    /// </summary>
    /// <value>The dependencies.</value>
    public String[] Dependencies {
      get;
      set;
    }

    /// <summary>
    /// Third party services or data stores with usage limitations
    /// </summary>
    /// <value>The resources.</value>
    public Dictionary<String, float> Resources {
      get;
      set;
    }

    /// <summary>
    /// Can the application connect to the database.
    /// </summary>
    /// <returns><c>true</c> if this instance can connect to database; otherwise, <c>false</c>.</returns>
    private bool CanConnectToDatabase()
    {
      try
      {
        using(var connection = Db.Connect()){};
        return true;
      }
      catch (Exception ex){
        Console.WriteLine (ex.ToString ());
        return false;
      }
    }

    /// <summary>
    /// Is everything working?
    /// </summary>
    /// <returns>The app for errors.</returns>
    private String CheckAppForErrors() {
      // Check error log, nancy has a catch all error
      // TODO: Nancy sends html on errors! But all is not lost, you can override
      if (CanConnectToDatabase())
        return "ok";
      else
        return "Unable to connect to database.";
    }
  }
}

