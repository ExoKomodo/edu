#! /bin/bash

set -eo pipefail

cd $(dirname ${BASH_SOURCE[0]})

CODE=0

set +e
./check_env_vars.sh
CODE=$?
set -e

if [[ -z ${AWS_SECRET_ACCESS_KEY} ]]; then
  echo "MISSING: AWS_SECRET_ACCESS_KEY is required to authenticate  for object storage"
  CODE=1
fi

if [[ -z ${MONGODB_DATABASE} ]]; then
  echo "MISSING: MONGODB_DATABASE is required for database access"
  CODE=1
fi

if [[ -z ${MONGODB_URI} ]]; then
  echo "MISSING: MONGODB_URI is required for database access"
  CODE=1
fi

set -u

DOTNET_VERSION=$(dotnet --version)
if [[ ${DOTNET_VERSION} != 7.* ]]; then
  echo "Unsupported version of dotnet: ${DOTNET_VERSION}"
  CODE=1
fi

PORT=5000
set +e
OUTPUT="$(lsof -i :${PORT})"
if [[ $? == 0 ]]; then
  echo "Something is already running on port ${PORT}! :("
  echo ""
  echo "${OUTPUT}"
  CODE=1
fi
set -e

if [[ CODE != "0" ]]; then
  exit ${CODE}
fi

echo "Environment checks out!"
