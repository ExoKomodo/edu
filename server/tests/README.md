# Server Tests

## Setup

### Connecting to Mongo

`MONGODB_URI` is specifically useful because the .NET library uses that env var

```shell
export MONGODB_URI="<connection string>"
# NOTE: Not needed, but connects you via an interactive JS shell
mongosh ${MONGODB_URI}
```

## Run

Without hot reloading:

```shell
dotnet test
```

With hot reloading:

```shell
dotnet watch test
```
