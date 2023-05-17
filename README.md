# Edu

## Important Links

- [Github Repository](https://github.com/ExoKomodo/Edu)
- [Deployed client](https://edu.exokomodo.com)
- [Deployed server](https://services.edu.exokomodo.com/api/v1)
- [Digital Ocean App](https://cloud.digitalocean.com/apps/49add3d3-1578-4b2a-916d-8c8b9a197fd4)
- [Spaces Host](https://edu.exokomodo.sfo3.digitaloceanspaces.com)

## Client

### Setup the client

Install nodejs v18+. Using [`nvm`](https://github.com/nvm-sh/nvm) is the best option.

After installing, refer to the [`README`](./client/README.md#project-setup)

### Run the client

Refer to the [`README`](./client/README.md#compile-and-hot-reload-for-development)

### Test the client

Refer to the [`README`](./client/README.md#run-unit-tests-with-vitest)

## Server

### Setup the server

Install [.Net 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

### Run the server

Refer to the [`README`](./server/README.md#run)

### Test the server

Install .Net 7. For everything else, refer to the [`README`](./server.tests/README.md)

## Object Storage

### Installing s3cmd

s3cmd is an open-source and platform-agnostic s3 CLI tool, having a superset of the `aws s3` subcommands features

[Digital Ocean reference on setting up s3cmd](https://docs.digitalocean.com/products/spaces/reference/s3cmd/)
```shell
$ curl -O -L https://github.com/s3tools/s3cmd/releases/download/v2.3.0/s3cmd-2.3.0.tar.gz
$ tar xzf s3cmd-2.3.0.tar.gz
$ cd s3cmd-2.3.0
$ mkdir ~/.local/bin
$ cp -R s3cmd S3 ~/.local/bin
# NOTE: python3 has to be available as `python` for s3cmd to work
$ s3cmd
ERROR: /home/jamesaaronorson/.s3cfg: No route to host
ERROR: Configuration file not available.
ERROR: Consider using --configure parameter to create one.
```

### Configuring s3cmd

```shell
$ export AWS_ACCESS_KEY_ID="DO00GJNKZJVF8EHF4N2T"
$ export SECRET_ACCESS_KEY="*************************************"
$ s3cmd --configure

Enter new values or accept defaults in brackets with Enter.
Refer to user manual for detailed description of all options.

Access key and Secret key are your identifiers for Amazon S3. Leave them empty for using the env variables.
Access Key [DO00GJNKZJVF8EHF4N2T]: 
Secret Key [*************************************]: 
Default Region [US]: 

Use "s3.amazonaws.com" for S3 Endpoint and not modify it to the target Amazon S3.
S3 Endpoint [s3.amazonaws.com]: sfo3.digitaloceanspaces.com

Use "%(bucket)s.s3.amazonaws.com" to the target Amazon S3. "%(bucket)s" and "%(location)s" vars can be used
if the target S3 system supports dns based buckets.
DNS-style bucket+hostname:port template for accessing a bucket [%(bucket)s.s3.amazonaws.com]: 

Encryption password is used to protect your files from reading
by unauthorized persons while in transfer to S3
Encryption password: 
Path to GPG program [/usr/bin/gpg]: 

When using secure HTTPS protocol all communication with Amazon S3
servers is protected from 3rd party eavesdropping. This method is
slower than plain HTTP, and can only be proxied with Python 2.7 or newer
Use HTTPS protocol [Yes]: 

On some networks all internet access must go through a HTTP proxy.

...

Test access with supplied credentials? [Y/n] y
Please wait, attempting to list all buckets...
Success. Your access key and secret key worked fine :-)

Now verifying that encryption works...
Not configured. Never mind.

Save settings? [y/N] y
Configuration saved to '/home/jamesaaronorson/.s3cfg'
$ s3cmd ls
2023-05-17 20:01  s3://edu.exokomodo
```
