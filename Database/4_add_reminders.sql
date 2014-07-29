-- drop table reminders;
create table "reminders" (
  id serial not null primary key,
  reminder_type integer references reminder_types(id),
  message text not null,
  remind_on timestamp without time zone,
  verified boolean default false,
  address text,
  created_at timestamp with time zone default localtimestamp
);

grant all on table reminders to denver_schedules;