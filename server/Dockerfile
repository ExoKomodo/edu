FROM mcr.microsoft.com/dotnet/sdk:7.0 as builder

COPY . /server

WORKDIR /server

RUN dotnet publish --configuration Release

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as deployer

COPY --from=builder /server /server

RUN apt-get update -qy
RUN apt-get install -qy \
  build-essential \
  gettext-base \
  git \
  libpcre3-dev \
  libssl-dev \
  wget \
  vim \
  zlib1g-dev

WORKDIR /install

RUN wget 'http://nginx.org/download/nginx-1.18.0.tar.gz'
RUN tar -xzvf nginx-1.18.0.tar.gz
RUN rm -f nginx-1.18.0.tar.gz
RUN chown -R root:root ./nginx-1.18.0

WORKDIR /install/nginx-1.18.0
RUN git clone --depth=1 --branch v0.3.2 https://github.com/simpl/ngx_devel_kit
RUN git clone --depth=1 --branch v0.33 https://github.com/openresty/set-misc-nginx-module
RUN ./configure \
  --prefix=/etc/nginx \
  --sbin-path=/usr/sbin/nginx \
  --with-http_ssl_module \
  --add-module=$(pwd)/ngx_devel_kit \
  --add-module=$(pwd)/set-misc-nginx-module

RUN rm -rf nginx-$NGINX_VERSION/

RUN make -j2
RUN make install

# RUN nginx

WORKDIR /server

ARG ACCESS_KEY_ID
# EXAMPLE: mongodb+srv://doadmin:<insert password here>@exokomodo-mongo-51924df0.mongo.ondigitalocean.com/admin?tls=true&authSource=admin
ARG MONGODB_URI
ARG SECRET_ACCESS_KEY

ENV AWS_ACCESS_KEY_ID=${ACCESS_KEY_ID}
ENV MONGODB_URI=${MONGODB_URI}
ENV AWS_SECRET_ACCESS_KEY=${SECRET_ACCESS_KEY}

# RUN rm -f /etc/nginx/sites-enabled/*
# RUN cat /server/nginx/server.conf | envsubst '$AWS_SECRET_ACCESS_KEY $AWS_ACCESS_KEY_ID' > /etc/nginx/sites-available/server.conf
# RUN ln -s /etc/nginx/sites-available/server.conf /etc/nginx/sites-enabled/server.conf

RUN touch ${HOME}/.s3cfg
RUN echo "[default]" >> ${HOME}/.s3cfg; \
  echo "bucket_location = US" >> ${HOME}/.s3cfg; \
  echo "check_ssl_certificate = True" >> ${HOME}/.s3cfg; \
  echo "check_ssl_hostname = True" >> ${HOME}/.s3cfg; \
  echo "host_base = sfo3.digitaloceanspaces.com" >> ${HOME}/.s3cfg; \
  echo "host_bucket = %(bucket)s.sfo3.digitaloceanspaces.com" >> ${HOME}/.s3cfg; \
  echo "" >> ${HOME}/.s3cfg;

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:5000

CMD ["bash", "./deploy.sh"]
