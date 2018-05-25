#!/usr/bin/env bash

# Recreate the DB using database-utility-svc:8090/storage/mongodb-result
mongorestore --host result-mongodb --port 27017 --archive=/data/result-mongodb.bson