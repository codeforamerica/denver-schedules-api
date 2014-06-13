-- Create User
create user denver_schedules with password 'denver_schedules';

-- Database: denver_schedules_development

-- DROP DATABASE denver_schedules_development;

CREATE DATABASE denver_schedules_development
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       LC_COLLATE = 'en_US.UTF-8'
       LC_CTYPE = 'en_US.UTF-8'
       CONNECTION LIMIT = -1;
GRANT CONNECT, TEMPORARY ON DATABASE denver_schedules_development TO public;

-- Grant Permissions
GRANT ALL ON DATABASE denver_schedules_development TO denver_schedules;

-- Add PostGIS Extension
\c denver_schedules_development;
create extension postgis; 