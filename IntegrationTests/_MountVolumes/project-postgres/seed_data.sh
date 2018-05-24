#!/usr/bin/env bash

pg_restore --host project-postgres --port 5432 --username projectsvc --password mariokart102 -f /data/data.sql --verbose