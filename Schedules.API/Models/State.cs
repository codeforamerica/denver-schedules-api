using System;
using Schedules.API.Extensions;
using System.Collections.Generic;

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
    /// Um...this is a lie at the moment
    /// </summary>
    /// <returns>The app for errors.</returns>
    private String CheckAppForErrors() {
      // Check connection to database
      // Check error log, nancy has a catch all error
      // TODO: Nancy sends html on errors! But all is not lost, you can override
      return "ok";
    }
  }
}

