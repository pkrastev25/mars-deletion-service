#!/usr/bin/env bash
# Used to build the docker container which contains all dependencies needed to run integration and end-to-end tests
# Then it pushed it to the Nexus

# HINT: In order to build the image from your local machine, use:
# docker login -u HAW_USER_NAME ${GITLAB_REGISTRY}, where HAW_USER_NAME is your account!

GITLAB_REGISTRY="docker-hub.informatik.haw-hamburg.de"
PROJECT="mars/mars-deletion-svc"
SERVICE_NAME="deletion-svc"

docker build -t ${GITLAB_REGISTRY}/${PROJECT}/${SERVICE_NAME} .
docker push ${GITLAB_REGISTRY}/${PROJECT}/${SERVICE_NAME}