#!/usr/bin/env bash
# Starts the end-to-end tests within deletion-svc-tests image in docker compose

cd mars-deletion-svc/EndToEndTests &&
dotnet test -c Release
