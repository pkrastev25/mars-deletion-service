#!/usr/bin/env bash

# Recreate the DB using database-utility-svc:8090/storage/mongodb-meta
mongorestore --host mongodb --port 27017 --archive=/data/mongodb.bson --drop