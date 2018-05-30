#!/usr/bin/env bash
# Used to build you application, push a docker image to Nexus and restart the service in your cluster

GITLAB_REGISTRY="docker-hub.informatik.haw-hamburg.de"
PROJECT="mars/mars-deletion-svc"
SERVICE_NAME="deletion-svc"

rm -rf out

dotnet publish -o out

cd ..

# In order to push images from your local machine, use:
#docker login -u HAW_USER_NAME -p HAW_PASSWORD
docker build -t ${GITLAB_REGISTRY}/${PROJECT}/${SERVICE_NAME}:dev .
docker push ${GITLAB_REGISTRY}/${PROJECT}/${SERVICE_NAME}:dev

kubectl delete pod -l service=${SERVICE_NAME} --force