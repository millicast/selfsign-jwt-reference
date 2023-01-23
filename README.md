# Dolby.io Streaming Self-signed JWT Reference

This repository shows how to create a self-signed subscriber token for use with [Dolby.io Real-time Streaming](https://dolby.io/products/real-time-streaming/).
See our [Self-signed token guide](https://docs.dolby.io/streaming-apis/docs/self-signed-tokens) for more information about self-signing tokens.

We can also track bandwidth usage with each of our self-signed tokens. See our [Tracking with self-signed tokens guide](https://docs.dolby.io/streaming-apis/docs/self-signed-tokens) or the [README](./tracking.md) for more information.

Once created, the token can be used as the bearer token in a request to the [Subscribe API](https://docs.dolby.io/streaming-apis/reference/director_subscribe)

The Master Subscribe Token (MST) can be created and looked up using our [REST API](https://apidocs.millicast.com/redoc/#tag/SubscribeToken).

The examples provided use the [`Read Subscribe Token`](https://apidocs.millicast.com/redoc/#operation/SubscribeToken_ReadToken) API response to create the Self-Signed Token (SST). These can be found [here](https://apidocs.millicast.com/redoc/#tag/SubscribeToken)
## Reference code

For each sample app, we have shown 3 scenarios for creating the SST.
1. Creating an SST with no tracking
2. Creating an SST using the MST's `TrackingId`
3. Creating an SST with a custom `TrackingId`

#### Sample Apps

* [C# .NET](./src/dotnet/README.md)
* [Java](./src/java/README.md)
* [Node.js](./src/nodejs/README.md)
* [Python](./src/python/README.md)
* [Ruby](./src/ruby/README.md)
* 
All samples have been tested on Ubuntu 22.04.

## Details

* For detailed information about the JWT and its fields, see [here](./jwt.md)
* For information about tracking inside the JWT, see [here](tracking.md)
* For how to add examples for other languages, see [here](./new_language.md)
