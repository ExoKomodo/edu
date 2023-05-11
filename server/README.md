# Server

## Connecting to mongo

`MONGODB_URI` is specifically useful because the .NET library uses that env var

```shell
export MONGODB_URI="<connection string>"
mongosh ${MONGODB_URI}
```

## Run

Without hot reloading:

```shell
dotnet run
```

With hot reloading:

```shell
dotnet watch run
```
