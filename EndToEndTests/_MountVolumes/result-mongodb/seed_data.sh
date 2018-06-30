#!/usr/bin/env bash

# database-utility-svc
mongorestore --drop --host result-mongodb --port 27017 --db ResultData --archive=/seed-data/ResultData.bson