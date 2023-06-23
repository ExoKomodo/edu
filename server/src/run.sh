#! /bin/bash

set -euo pipefail

cd $(dirname ${BASH_SOURCE[0]})

./check_env.sh

echo "Installing dotnet dependencies..."
dotnet restore
echo "Done installing dotnet dependencies!"

echo "Running server with hot reloading (watch)..."
dotnet watch run
