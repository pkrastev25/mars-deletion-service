#!/usr/bin/env bash
# Used to run the integration test locally on your machine

docker-compose down &&
# Clear any legacy volumes
docker volume prune &&
# For a more interactive output, use:
#docker-compose up
docker-compose run deletion-svc-tests