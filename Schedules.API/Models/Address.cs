using System;

namespace Schedules.API.Models
{
  public class Address
  {
    public Address ()
    {
    }

    public double Longitude {
      get;
      set;
    }

    public double Latitude {
      get;
      set;
    }

    public double Accuracy {
      get;
      set;
    }
  }
}

