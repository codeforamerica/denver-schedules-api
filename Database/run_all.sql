\ir 1_create_psql_database.sql
\ir 2_add_street_sweeping_routes.sql
\ir 3_add_reminder_types.sql
\ir 4_add_reminders.sql
grant all on all tables in schema public to denver_schedules;
grant all on all sequences in schema public to denver_schedules;