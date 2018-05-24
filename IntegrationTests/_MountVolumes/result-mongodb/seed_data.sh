#!/usr/bin/env bash

# database-utility-svc
mongoimport --host result-mongodb --port 27017 --db ResultData --collection 4ab312c4-801d-4fa1-b8d9-4e9b24c5dd4a-kf --type json --file /data/4ab312c4-801d-4fa1-b8d9-4e9b24c5dd4a-kf.json --jsonArray &&
mongoimport --host result-mongodb --port 27017 --db ResultData --collection 4ab312c4-801d-4fa1-b8d9-4e9b24c5dd4a-meta --type json --file /data/4ab312c4-801d-4fa1-b8d9-4e9b24c5dd4a-meta.json --jsonArray &&
mongoimport --host result-mongodb --port 27017 --db ResultData --collection SimulationRuns --type json --file /data/SimulationRuns.json --jsonArray