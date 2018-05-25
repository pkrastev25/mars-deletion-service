#!/usr/bin/env bash

# Recreate the DB using database-utility-svc:8090/storage/postgres-projects
PGPASSWORD=mariokart102 psql --host project-postgres --port 5432 -U projectsvc -d project -f /data/project-postgres.sql