using Nancy;

public class SchedulesModule : NancyModule
{
	public SchedulesModule()
	{
		Get["/"] = parameters => "Hello Worlds";
	}
}
