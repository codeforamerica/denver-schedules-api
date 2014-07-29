-- drop table reminder_types;
create table "reminder_types" (
  id serial not null primary key,
  name varchar(80),
  description text
);

insert into "reminder_types" ("name", "description") values ('sms', 'Send a text message reminder. Phone number is required.');
insert into "reminder_types" ("name", "description") values ('email', 'Send an email reminder. Email address is required.');
insert into "reminder_types" ("name", "description") values ('yo', 'Send a yo. Yo username is required.');

grant all on table reminder_types to denver_schedules;
