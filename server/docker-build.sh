#! /bin/bash

source ./setup.sh

docker build --build-arg MONGODB_URI=${MONGODB_URI} --build-arg ACCESS_KEY_ID=${AWS_ACCESS_KEY_ID} --build-arg SECRET_ACCESS_KEY=${AWS_SECRET_ACCESS_KEY} .
