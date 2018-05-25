#!/usr/bin/env bash

# file-svc
mongorestore --drop --host mongodb --port 27017 -db importFiles --archive=/seed-data/importFiles.bson

# metadata -svc
mongorestore --drop --host mongodb --port 27017 -db import --archive=/seed-data/import.bson

# scenario-svc
mongorestore --drop --host mongodb --port 27017 -db mars_websuite --archive=/seed-data/mars_websuite.bson

# resultcfg-svc
mongorestore --drop --host mongodb --port 27017 -db Configs --archive=/seed-data/Configs.bson

# sim-runner
mongorestore --drop --host mongodb --port 27017 -db mars-mission-control --archive=/seed-data/mars-mission-control.bson