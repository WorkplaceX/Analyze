#!/bin/sh -l

set -x # Enable print execute cammands to stdout.
BASH_XTRACEFD=1 # Print execute command to stdout. Not to stderr.

echo "Hello from Docker 2"
cat /etc/os-release

echo "myparam=$1"