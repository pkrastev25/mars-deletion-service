# Include dotnet SDK
FROM nexus.informatik.haw-hamburg.de/microsoft/dotnet:2.0.0-sdk

# Install docker
RUN apt-get update && apt-get install -y apt-transport-https ca-certificates wget software-properties-common
RUN wget https://download.docker.com/linux/debian/gpg 
RUN apt-key add gpg
RUN echo "deb [arch=amd64] https://download.docker.com/linux/debian $(lsb_release -cs) stable" | tee -a /etc/apt/sources.list.d/docker.list

# Install docker-compose
RUN apt-get update && apt-cache policy docker-ce
RUN apt-get -y install docker-ce
RUN curl -L https://github.com/docker/compose/releases/download/1.21.2/docker-compose-$(uname -s)-$(uname -m) -o /usr/local/bin/docker-compose
RUN chmod +x /usr/local/bin/docker-compose