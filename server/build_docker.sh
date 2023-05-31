#! /bin/bash

set -euo pipefail

cd $(dirname ${BASH_SOURCE[0]})

./check_env_vars.sh

docker build \
  --build-arg MONGODB_URI=${MONGODB_URI} \
  --build-arg ACCESS_KEY_ID=${AWS_ACCESS_KEY_ID} \
  --build-arg SECRET_ACCESS_KEY=${AWS_SECRET_ACCESS_KEY} \
  .
