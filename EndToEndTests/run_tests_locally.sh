#!/usr/bin/env bash
# Used to run the end-to-end tests locally on your machine

docker-compose down &&
# Clear any legacy volumes
docker volume prune &&
# For a more interactive output, use:
#docker-compose up
docker-compose run deletion-svc-tests