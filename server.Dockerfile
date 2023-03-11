############
# Builder #
############
FROM mcr.microsoft.com/dotnet/sdk:7.0 as builder

COPY ./server /server

WORKDIR /server

RUN dotnet publish --configuration Release

############
# Deployer #
############
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as deployer

COPY --from=builder /server /server

RUN apt-get update -y
RUN apt-get install nginx -y

WORKDIR /server

RUN rm -f /etc/nginx/sites-enabled/*
RUN ln -f ./nginx/server.conf /etc/nginx/sites-available/server.conf
RUN ln -s /etc/nginx/sites-available/server.conf /etc/nginx/sites-enabled/server.conf

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:5000

CMD ["bash", "./deploy.sh"]
