#! /bin/bash

set -eo pipefail

cd $(dirname ${BASH_SOURCE[0]})

CODE=0

if [[ -z ${AWS_ACCESS_KEY_ID} ]]; then
  echo "MISSING: AWS_ACCESS_KEY_ID is required to authenticate for object storage" 
  CODE=1
fi

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

exit ${CODE}
