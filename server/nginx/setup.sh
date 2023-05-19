#! /bin/bash

set -euo pipefail

cd $(dirname ${BASH_SOURCE[0]})

NGINX_CONF=/usr/local/openresty/nginx/conf/nginx.conf

cp ${NGINX_CONF} ${NGINX_CONF}.bak
rm -f ${NGINX_CONF}
ln -f $(pwd)/nginx.conf ${NGINX_CONF}
