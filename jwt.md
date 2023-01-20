# JWT details

## The payload

The payload inside the JWT should always follow this layout: casing **does matter here**. All options should be
contained inside a field named `streaming`. `tokenType` currently always defaults to `Subscribe`. As mentioned
previously `allowedOrigins` and `allowedIpAddresses` should default to an empty array if not specified.

```psuedojson
{
  "streaming": {
    "tokenId": number,
    "tokenType": "Subscribe",
    "streamName": string,
    "allowedOrigins": string[],
    "allowedIpAddresses": string[]
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

```json
{
  "streaming": {
    "tokenId": 1,
    "tokenType": "Subscribe",
    "streamName": "teststream",
    "allowedOrigins": [],
    "allowedIpAddresses": []
  },
  "exp": 1673600917,
  "iat": 1673600857
}
```
