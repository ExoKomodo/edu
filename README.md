# Edu

## Important Links

- [Github Repository](https://github.com/ExoKomodo/Edu)
- [Deployed client](https://edu.exokomodo.com)
- [Deployed server](https://services.edu.exokomodo.com/api/v1)
- [Digital Ocean App](https://cloud.digitalocean.com/apps/49add3d3-1578-4b2a-916d-8c8b9a197fd4)
- [Spaces Host](https://edu.exokomodo.sfo3.digitaloceanspaces.com)
- [AWS S3 Installation Guide](https://docs.aws.amazon.com/cli/latest/userguide/getting-started-install.html)

## [Client](./client/)

### [Client](./client/) - Setup

Install nodejs v18+. Using [`nvm`](https://github.com/nvm-sh/nvm) is the best option.

After installing, refer to the [`README`](./client/README.md#project-setup)

### [Client](./client/) - Run

Refer to the [`README`](./client/README.md#compile-and-hot-reload-for-development)

### [Client](./client/) - Test

Refer to the [`README`](./client/README.md#run-unit-tests-with-vitest)

## [Server](./server/)

### [Server](./server/src) - Setup

Install [.Net 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

```bash
# Setup mongo creds. Feel free to make your own mongodb database and connect to that.
export MONGODB_URI="<connection string>"
# Setup object storage creds
export AWS_ACCESS_KEY_ID="DO00MM6Z43GZ4V6YX8PY"
export AWS_SECRET_ACCESS_KEY="*************************************"
```

### [Server](./server/src) - Run

#### Using [helper script](./server/src/run.sh)

The helper script will verify the environment you are running in. By doing so, you should be able to successfully run the server, as long as this script can run. If the script cannot successfully run, the script will proactively tell you what is wrong with the environment and why.

```shell
./run.sh
```

#### Server run on your own

Without hot reloading:

```shell
dotnet restore # Optional step, as `dotnet run` will restore as well
dotnet run
```

With hot reloading:

```shell
# Optional step, as `dotnet run` will restore as well
dotnet watch run
```

### [server/tests](./server/tests/) - Test

#### Using [helper script](./server/tests/test.sh)

The helper script will verify the environment you are running in. By doing so, you should be able to successfully run the server, as long as this script can run. If the script cannot successfully run, the script will proactively tell you what is wrong with the environment and why.

```shell
./test.sh
```

#### Server test on your own

Without hot reloading:

```shell
dotnet restore # Optional step, as `dotnet test` will restore as well
dotnet test
```

With hot reloading:

```shell
# Optional step, as `dotnet test` will restore as well
dotnet watch test
```

### [Server](./server/src) - Mongo

#### Interacting with Mongo

Mongo works with a simple pattern

You have:

1. a client, that connects to
1. a database, that opens
1. a collection of documents, that can be
1. filtered, and those elements can now be either
    - retrieved
    - updated

##### Connecting to a collection

A collection is somewhat analogous to a table in a normal SQL database

```fsharp
let connectionString = Environment.GetEnvironmentVariable("MONGODB_URI")
match connectionString with
| null ->
  printfn "You must set your 'MONGODB_URI' environmental variable. See\n\t https://www.mongodb.com/docs/drivers/go/current/usage-examples/#environment-variable"
  exit 1
| _ ->
  let client = new MongoClient(connectionString)
  let database = client.GetDatabase("edu")
  let collection = database.GetCollection<Course>("courses")
```

##### Filtering items

Assuming the previous sections's `collection` will receive the `filter`

Any operations needs to be filtered, so it only applies to that subset of documents, whether it be that the operation is `Get` or `Update`

```fsharp
let filter = Builders<Course>.Filter.Eq("Id", "intro")
let course = collection.Find(filter).FirstOrDefault()
// NOTE: Must use `box` to convert value type like `Course`, into a reference so null can be checked
match box course with
| null -> RequestErrors.NOT_FOUND $"Course not found with id {id}"
| _ -> json course
```

You can make a `filter` without a `collection`. It is simply a piece of data describing a `filter` to apply.

##### Updating items

Assuming the previous section's `filter`

```fsharp
// NOTE: Should be able to chain together .Set() calls to update multiple fields at once
let update = Builders<Course>.Update.Set(
  // Pick, in an anonymous function, what field to update
  (fun course -> course.Metadata.Description),
  "Introducing you to the Edu platform, where higher learning persuades you"
)
// Filter to the element to effect, then apply the update, and ignore the results
collection.UpdateOne(filter, update) |> ignore
```

### [Server](./server/src) - Object Storage (s3)

#### Installing aws cli

Follow the instructions found [here](https://docs.aws.amazon.com/cli/latest/userguide/getting-started-install.html)

#### Using s3

```shell
# Test that you can connect to s3
aws s3 ls edu.exokomodo --endpoint-url https://sfo3.digitaloceanspaces.com
```
