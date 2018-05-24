#!/usr/bin/env bash
# Used to build the docker container which contains all dependencies needed to run integration and end-to-end tests
# Then it pushed it to the Nexus

DOCKER_REGISTRY="nexus.informatik.haw-hamburg.de"
SERVICE_NAME="deletion-svc-test-env"

docker build -t ${DOCKER_REGISTRY}/${SERVICE_NAME} .
docker push ${DOCKER_REGISTRY}/${SERVICE_NAME}