
-- Database: denver_schedules_development
-- Disconnect everyone
select pg_terminate_backend(pg_stat_activity.pid)
from pg_stat_activity
where pg_stat_activity.datname = 'denver_schedules_development'
  and pid <> pg_backend_pid();

-- Drop the db
drop database if exists denver_schedules_development;
create database denver_schedules_development
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       LC_COLLATE = 'en_US.UTF-8'
       LC_CTYPE = 'en_US.UTF-8'
       CONNECTION LIMIT = -1;
grant connect, temporary on database denver_schedules_development to public;

-- Create User
drop user if exists denver_schedules;
create user denver_schedules with password 'denver_schedules';

-- Grant Permissions
grant all on database denver_schedules_development to denver_schedules;

-- Add PostGIS extension
\c denver_schedules_development;
create extension postgis;
