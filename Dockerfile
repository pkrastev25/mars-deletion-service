FROM nexus.informatik.haw-hamburg.de/microsoft/aspnetcore:2.0

# Reflection service API runs on port 8080.
EXPOSE 8080

# Copy the sources into the Docker container.
ADD ./out /out 

# Build and run the program on container startup.
WORKDIR /out

ENTRYPOINT dotnet mars-deletion-svc.dll