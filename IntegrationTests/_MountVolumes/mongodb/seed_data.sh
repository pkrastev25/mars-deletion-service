#!/usr/bin/env bash

# file-svc
mongorestore --gzip --host mongodb --port 27017 -db importFiles --archive=/data/importFiles.agz &&

# metadata -svc
mongorestore --gzip --host mongodb --port 27017 -db import --archive=/data/import.agz &&

# scenario-svc
mongorestore --gzip --host mongodb --port 27017 -db mars_websuite --archive=/data/mars_websuite.agz &&

# resultcfg-svc
mongorestore --gzip --host mongodb --port 27017 -db Configs --archive=/data/Configs.agz &&

# sim-runner
mongorestore --gzip --host mongodb --port 27017 -db mars-mission-control --archive=/data/mars-mission-control.agz