#!/usr/bin/env bash

# file-svc
mongoimport --host mongodb --port 27017 --db importFiles --collection fs.chunks --type json --file /data/fs.chunks.json --jsonArray &&
mongoimport --host mongodb --port 27017 --db importFiles --collection fs.files --type json --file /data/fs.files.json --jsonArray

# metadata-svc
mongoimport --host mongodb --port 27017 --db import --collection metadata --type json --file /data/metadata.json --jsonArray &&

# scenario-svc
mongoimport --host mongodb --port 27017 --db mars_websuite --collection mapping_index --type json --file /data/mapping_index.json --jsonArray &&
mongoimport --host mongodb --port 27017 --db mars_websuite --collection scenario_metadata --type json --file /data/scenario_metadata.json --jsonArray &&
mongoimport --host mongodb --port 27017 --db mars_websuite --collection scenario_parameter_sets --type json --file /data/scenario_parameter_sets.json --jsonArray &&

# resultcfg-svc
mongoimport --host mongodb --port 27017 --db Configs --collection OutputConfigs --type json --file /data/OutputConfigs.json --jsonArray &&
mongoimport --host mongodb --port 27017 --db Configs --collection VisualizationConfigs --type json --file /data/VisualizationConfigs.json --jsonArray &&

# sim-runner-svc
mongoimport --host mongodb --port 27017 --db mars-mission-control --collection SimulationPlans --type json --file /data/SimulationPlans.json --jsonArray &&
mongoimport --host mongodb --port 27017 --db mars-mission-control --collection SimulationRuns --type json --file /data/SimulationRuns.json --jsonArray &&

# marking-svc
mongoimport --host mongodb --port 27017 --db marking-svc --collection mark-session --type json --file /data/mark-session.json --jsonArray &&

# deletion-svc
mongoimport --host mongodb --port 27017 --db hangfire-marking-svc --collection hangfire.stateData --type json --file /data/hangfire.stateData.json --jsonArray