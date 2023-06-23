#! /bin/bash

set -euo pipefail

openresty

cd /server/src/bin/Release/net7.0

./server
