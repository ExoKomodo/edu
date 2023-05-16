# Server

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
dotnet run
```

With hot reloading:

```shell
dotnet watch run
```

## Interacting with Mongo in Code

Mongo works with a simple pattern

You have:

1. a client, that connects to
1. a database, that opens
1. a collection of documents, that can be
1. filtered, and those elements can now be either
    * retrieved
    * updated

### Connecting to a collection

A collection is somewhat analogous to a table in a normal SQL database

```fsharp
let connectionString = Environment.GetEnvironmentVariable("MONGODB_URI")
match connectionString with
| null ->
  printfn "You must set your 'MONGODB_URI' environmental variable. See\n\t https://www.mongodb.com/docs/drivers/go/current/usage-examples/#environment-variable"
  exit 1
| _ ->
  let client = new MongoClient(connectionString)
  let database = client.GetDatabase("admin")
  let collection = database.GetCollection<Course>("courses")
```

### Filtering items

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

### Updating items

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
