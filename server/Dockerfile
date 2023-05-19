###########
# BUILDER #
###########
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS builder

COPY . /server

WORKDIR /server

RUN dotnet publish --configuration Release

##########
# RUNNER #
##########
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runner

COPY --from=builder /server /server

RUN apt-get update -qy
RUN apt-get -y install --no-install-recommends \
  ca-certificates \
  gnupg \
  vim \
  wget
RUN wget -O - https://openresty.org/package/pubkey.gpg | apt-key add -
RUN echo "deb http://openresty.org/package/debian bullseye openresty" | tee /etc/apt/sources.list.d/openresty.list
RUN apt-get update -qy

RUN apt-get install -qy --no-install-recommends \
  openresty

WORKDIR /server

ARG ACCESS_KEY_ID
# EXAMPLE: mongodb+srv://doadmin:<insert password here>@exokomodo-mongo-51924df0.mongo.ondigitalocean.com/admin?tls=true&authSource=admin
ARG MONGODB_URI
ARG SECRET_ACCESS_KEY

ENV AWS_ACCESS_KEY_ID=${ACCESS_KEY_ID}
ENV MONGODB_URI=${MONGODB_URI}
ENV AWS_SECRET_ACCESS_KEY=${SECRET_ACCESS_KEY}

RUN ./nginx/setup.sh

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:5000

CMD ["bash", "./docker-run.sh"]
