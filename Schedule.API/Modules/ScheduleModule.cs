using Nancy;

public class ScheduleModule : NancyModule
{
	public ScheduleModule()
	{
		Get["/"] = parameters => "Hello World";
	}
}
