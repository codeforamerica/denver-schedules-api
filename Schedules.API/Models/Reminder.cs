using System;

namespace Schedules.API.Models
{
  /// <summary>
  /// Citizen wants a reminder about a city service :)
  /// </summary>
  public class Reminder : IEquatable<Reminder>
  {
    public Reminder ()
    {
      Contact = string.Empty;
      Message = string.Empty;
      Address = string.Empty;
      ReminderType = new ReminderType ();
    }

    public int Id { get; set; }

    public ReminderType ReminderType { get; set; }

    public int ReminderTypeId {
      get {
        return ReminderType.Id;
      }
    }
    public String Contact { get; set; }

    public String Message { get; set; }

    public DateTime RemindOn { get; set; }

    public Boolean Verified { get; set; }

    public String Address { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool Equals (Reminder other)
    {
      if (other == null)
        return false;

      return this.ReminderType.Name == other.ReminderType.Name
        && this.Contact == other.Contact
        && this.Message == other.Message
        && this.Verified == other.Verified
        && this.Address == other.Address;
    }

    public override bool Equals (Object obj)
    {
      return Equals(obj as Reminder);
    }

    public override int GetHashCode ()
    {
      return base.GetHashCode();
    }
  }
}
