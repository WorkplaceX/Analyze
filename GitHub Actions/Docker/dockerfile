# Container image that runs your code
FROM alpine:3.10

# Copies your code file from your action repository to the filesystem path `/` of the container
COPY main.sh /main.sh

# Code file to execute when the docker container starts up (`main.sh`)
ENTRYPOINT ["/main.sh"]
