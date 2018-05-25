#!/usr/bin/env bash

# database-utility-svc
mongorestore --gzip --host result-mongodb --port 27017 -db ResultData --archive=/data/ResultData.agz.agz