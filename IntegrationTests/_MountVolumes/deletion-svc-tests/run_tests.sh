#!/usr/bin/env bash
# Starts the integration tests within deletion-svc-tests image in docker compose

cd mars-deletion-svc/IntegrationTests
dotnet test -c Release
