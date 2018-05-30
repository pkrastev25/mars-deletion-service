#!/usr/bin/env bash
# Used to build the docker container which contains all dependencies needed to run integration and end-to-end tests
# Then it pushed it to the Nexus

GITLAB_REGISTRY="docker-hub.informatik.haw-hamburg.de"
PROJECT="mars/mars-deletion-svc"
SERVICE_NAME="deletion-svc

# In order to push images from your local machine, use:
#docker login -u HAW_USER_NAME -p HAW_PASSWORD
docker build -t ${GITLAB_REGISTRY}/${PROJECT}/${SERVICE_NAME} .
docker push ${GITLAB_REGISTRY}/${PROJECT}/${SERVICE_NAME}