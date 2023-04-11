# JWT details

## The payload

The payload inside the self-signed JWT (SST) should always follow this layout: casing **does matter here**. All options should be
contained inside a field named `streaming`. `tokenType` currently always defaults to `Subscribe`. As mentioned
previously `allowedOrigins` and `allowedIpAddresses` should default to an empty array if not specified.

We can have a `tracking` section as well, which can be used to track bandwidth usage of viewers using the `SST`.
If you can do not care about `tracking`, and your Master Subscribe Token does not have one, then we can leave `tracking` as `null`. The `SST` will not be tracked in this scenario.
There are some scenarios where the payload will need a `tracking` section (such as if it's already set on the Master Subscribe Token).
Read more [here](./tracking.md)

```psuedojson
{
  "streaming": {
    "tokenId": number,
    "tokenType": "Subscribe",
    "streamName": string,
    "allowedOrigins": string[],
    "allowedIpAddresses": string[],
    "tracking": null
  }
}
```

## Expected Format of Decoded JWT

The final JWT should resemble the following after being decoded with a tool like [jwt.io](http://jwt.io).

## Supported Algorithms

The default algorithm in most libraries is likely to be HS256.
We support the following: HS256, HS384, HS512.

```json
{
  "alg": "HS256",
  "typ": "JWT"
}
```

## Payload Example

The JWT signing library may or may not add a `nbf` (NotBefore) field.
This is supported, but not strictly required in our JWTs.

This example below shows an example `SST` with **no tracking**, generated from a Master Subscribe Token that does not have `tracking` enabled.
For other tracking examples, read [here](./tracking.md)

```json
{
  "streaming": {
    "tokenId": 1,
    "tokenType": "Subscribe",
    "streamName": "teststream",
    "allowedOrigins": [],
    "allowedIpAddresses": [],
    "tracking": null
  },
  "exp": 1673600917,
  "iat": 1673600857
}
```
```
