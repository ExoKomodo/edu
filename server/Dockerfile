FROM mcr.microsoft.com/dotnet/sdk:7.0 as builder

COPY . /server

WORKDIR /server

RUN dotnet publish --configuration Release

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as deployer

COPY --from=builder /server /server

RUN apt-get update -qy
RUN apt-get install nginx -qy

WORKDIR /server

RUN rm -f /etc/nginx/sites-enabled/*
RUN ln -f /server/nginx/server.conf /etc/nginx/sites-available/server.conf
RUN ln -s /etc/nginx/sites-available/server.conf /etc/nginx/sites-enabled/server.conf

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:5000

ARG ACCESS_KEY_ID
# EXAMPLE: mongodb+srv://doadmin:<insert password here>@exokomodo-mongo-51924df0.mongo.ondigitalocean.com/admin?tls=true&authSource=admin
ARG MONGODB_URI
ARG SECRET_ACCESS_KEY

ENV AWS_ACCESS_KEY_ID=${ACCESS_KEY_ID}
ENV MONGODB_URI=${MONGODB_URI}
ENV AWS_SECRET_ACCESS_KEY=${SECRET_ACCESS_KEY}
RUN touch ${HOME}/.s3cfg
RUN echo "[default]" >> ${HOME}/.s3cfg; \
  echo "bucket_location = US" >> ${HOME}/.s3cfg; \
  echo "check_ssl_certificate = True" >> ${HOME}/.s3cfg; \
  echo "check_ssl_hostname = True" >> ${HOME}/.s3cfg; \
  echo "host_base = sfo3.digitaloceanspaces.com" >> ${HOME}/.s3cfg; \
  echo "host_bucket = %(bucket)s.sfo3.digitaloceanspaces.com" >> ${HOME}/.s3cfg; \
  echo "" >> ${HOME}/.s3cfg;

CMD ["bash", "./deploy.sh"]
