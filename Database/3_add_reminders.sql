-- DROP TABLE reminders;

create table "reminders" (
	id serial NOT NULL,
	email varchar(80),
	cell varchar(80),
	message text,
	remind_on timestamp without time zone,
	email_verified boolean default false,
	cell_verified boolean default false,
	address text,
	created_at timestamp with time zone default localtimestamp
);
ALTER TABLE "reminders" ADD PRIMARY KEY (id);

