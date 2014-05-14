using Nancy;

public class SchedulesModule : NancyModule
{
	public SchedulesModule()
	{
		Get["/schedules"] = _ =>
		{
			return Response.AsJson(@"{
			title: 'City Holidays',
			events: [
				{
					desc: 'New Years Day',
					day: 'Wednesday',
					startdate: '01.01.2014'
				},
				{
					desc: 'Martin Luther King Day',
					day: 'Monday',
					startdate: '01.20.2014'
				},
				{
					desc: 'President's Day',
					day: 'Monday',
					startdate: '02.27.2014'
				},
				{
					desc: 'Cesar Chavez Day',
					day: 'Monday',
					startdate: '03.31.2014'
				},
				{
					desc: 'Memorial Day',
					day: 'Monday',
					startdate: '05.26.2014'
				},
				{
					desc: 'Independence Day',
					day: 'Friday',
					startdate: '07.04.2014'
				},
				{
					desc: 'Labor Day',
					day: 'Monday',
					startdate: '11.01.2014'
				},
				{
					desc: 'Thanksgiving Day',
					day: 'Thursday',
					startdate: '11.27.2014'
				},
				{
					desc: 'Christmas Day',
					day: 'Monday',
					startdate: '12.25.2014'
				}
		]");
		};
	}
}