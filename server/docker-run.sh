#! /bin/bash

set -euo pipefail

openresty

cd /server/bin/Release/net7.0

./server
