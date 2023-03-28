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
RUN echo $(ls -l /server)
RUN echo $(ls -l /server/nginx)
RUN ln -f /server/nginx/server.conf /etc/nginx/sites-available/server.conf
RUN ln -s /etc/nginx/sites-available/server.conf /etc/nginx/sites-enabled/server.conf

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:5000

CMD ["bash", "./deploy.sh"]
