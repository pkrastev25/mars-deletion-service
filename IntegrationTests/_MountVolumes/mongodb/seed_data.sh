#!/usr/bin/env bash

# file-svc
mongorestore --host mongodb --port 27017 -db importFiles --archive=/data/importFiles.archive &&

# metadata -svc
mongorestore --host mongodb --port 27017 -db import --archive=/data/import.archive &&

# scenario-svc
mongorestore --host mongodb --port 27017 -db mars_websuite --archive=/data/mars_websuite.archive &&

# resultcfg-svc
mongorestore --host mongodb --port 27017 -db Configs --archive=/data/Configs.archive &&

# sim-runner
mongorestore --host mongodb --port 27017 -db mars-mission-control --archive=/data/mars-mission-control.archive