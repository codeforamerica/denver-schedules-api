using System;
using Simpler;
using NUnit.Framework;
using Schedules.API.Tasks.Reminders;

namespace Schedules.API.Tests.Tasks.Reminders
{
  [TestFixture]
  public class FetchRemindersForContactTests:CreateRemindersTestsBase
  {
    private FetchRemindersForContact fetchRemindersForContact;

    [TestFixtureTearDown]
    public void TearDown()
    {
      TearDownEmail();
      TearDownSMS();
    }

    [Test]
    public void ShouldRetrieveOneSMSReminder()
    {
      SetUpSms();
      SetUpSms();
      FetchReminders(SmsContact);
      Assert.That(fetchRemindersForContact.Out.RemindersForContact.Count, Is.EqualTo(1));
    }

    [Test]
    public void ShouldRetrieveTwoSMSReminders()
    {
      SetUpSms();
      SetUpSms("Another reminder");
      FetchReminders(SmsContact);
      Assert.That(fetchRemindersForContact.Out.RemindersForContact.Count, Is.EqualTo(2));
    }

    [Test]
    public void ShouldRetrieveOneEmailReminder()
    {
      SetUpEmail();
      SetUpEmail();
      FetchReminders(EmailContact);
      Assert.That(fetchRemindersForContact.Out.RemindersForContact.Count, Is.EqualTo(1));
    }

    [Test]
    public void ShouldRetrieveTwoEmailReminders()
    {
      SetUpEmail();
      SetUpEmail("Another reminder");
      FetchReminders(EmailContact);
      Assert.That(fetchRemindersForContact.Out.RemindersForContact.Count, Is.EqualTo(2));
    }

    private void FetchReminders(string contact)
    {
      fetchRemindersForContact = Task.New<FetchRemindersForContact>();
      fetchRemindersForContact.In.Contact = contact;
      fetchRemindersForContact.Execute();
    }
  }
}

