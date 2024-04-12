###########
# BUILDER #
###########
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder

COPY . /server

WORKDIR /server/src

RUN dotnet publish --configuration Release

##########
# RUNNER #
##########
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runner

COPY --from=builder /server /server

RUN apt-get update -qy
RUN apt-get -y install --no-install-recommends \
  apt-transport-https \
  ca-certificates \
  gnupg \
  vim \
  wget
RUN wget -O - https://openresty.org/package/pubkey.gpg | apt-key add -
RUN echo "deb http://openresty.org/package/debian bullseye openresty" | tee /etc/apt/sources.list.d/openresty.list
RUN apt-get update -qy

RUN apt-get install -qy --no-install-recommends \
  openresty

WORKDIR /server/nginx

RUN ./setup.sh

WORKDIR /server/src

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:5000

CMD ["bash", "./docker-run.sh"]
