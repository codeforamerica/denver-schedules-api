using System.Collections.Generic;
using Nancy.Security;

namespace Schedules.API.Models
{
  public class UserIdentity : IUserIdentity
  {
    public string UserName { get; set; }
    public IEnumerable<string> Claims { get; set; }
  }
}
