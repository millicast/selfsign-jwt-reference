# Dolby.io Streaming Self-signed JWT Reference

This repository shows how to create a self-signed subscriber token for use with [Dolby.io Real-time Streaming](https://dolby.io/products/real-time-streaming/).
See our [Self-signed token guide](https://docs.dolby.io/streaming-apis/docs/self-signed-tokens) for more infromation about self-signing tokens.

Once created, the token can be used as the bearer token in a request to the [Subscribe API](https://docs.dolby.io/streaming-apis/reference/director_subscribe)

## Reference code

* [C# .NET](./src/dotnet/README.md)
* [Java](./src/java/README.md)
* [Node.js](./src/nodejs/README.md)
* [Python](./src/python/README.md)
* [Ruby](./src/ruby/README.md)

All samples have bene tested on Ubuntu 22.04.

## Details

For detailed information about the JWT and its fields see [here](./jwt.md)
For how to add examples for other languages see [here](./new_language.md)
