using System;
using Schedules.API.Extensions;
using NUnit.Framework;

namespace Schedules.API.Tests
{
  public class UnixTimeStampTest
  {
    int timeStamp = 1401250115;
    DateTime theDate = new DateTime(2014, 5, 28, 4, 8, 35, DateTimeKind.Utc);

    [Test ()]
    public void DateShouldConvertToUnixTimestamp()
    {
      var result = theDate.ToUnixTimestamp();
      Assert.That(result, Is.EqualTo(timeStamp));
    }

    [Test ()]
    public void UnixTimestampShouldConvertToDateTime()
    {
      var result = timeStamp.ToDateTime();
      Assert.That(result, Is.EqualTo(theDate));
    }
  }
}

