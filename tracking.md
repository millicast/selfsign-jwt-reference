# Tracking of your self-signed token


## Understanding Tracking
We now have the functionality to track bandwidth statistics across self-signed tokens (SST). The way we do this is by a `Tracking` section in the SST and setting a specific `TrackingId`.
Any owner of the **Master Subscribe Token (MST)** can then view bandwidth usage for per SST using our APIs. For Analytics API Documentation, see [here](https://apidocs.millicast.com/redoc/#tag/Analytics)

If the `MST` has no `TrackingId` present, and you don't care about tracking, then **you do not** need to specify one in the `SST`. However, the `SST` will **not be tracked** for analytics.

## Tracking rules

There are some rules that need to be followed in order for the SST to work with `Tracking`.
* If the `MST` has no `TrackingId` present, then your `SST` can have **any** `TrackingId` you wish
* If the `MST` has a `TrackingId`, then your `SST` will need to be set with the **same** `TrackingId`
* The `SST` created can only have one `Stream Name` which will need to be matched to a `Stream Name` from the `MST`
  * If the `MST` has global `".*"` with regex, then the `Stream Name` can be any `Stream Name`
  * If the `MST ` has specific streams in it, then the `SST` **must match one of them**.
  
## Verifying the Token

Verifying the payload can be done with a decoding tool like [jwt.io](http://jwt.io)

## Examples

If you don't want `tracking` on your SST, an example can be found [here](jwt.md)

### Tracking using a custom TrackingId
* If the `MST` has no `TrackingID`, then the `SST` can have a custom `TrackingId` of your choosing.

#### Master Subscribe Token Example with no tracking
```json
{
    "tokenId": 1,
    "token": "abcdefghijklmnopqrstuvwxyz012345678jh9",
    "streams": [
        {
            "streamName": "testStream"
        }
    ],
    "tracking": null
}
````

#### SST Example with custom tracking

```json
{
  "streaming": {
    "tokenId": 1,
    "tokenType": "Subscribe",
    "streamName": "teststream",
    "allowedOrigins": [],
    "allowedIpAddresses": [],
    "tracking": {
      "trackingId": "customTrackingId"
    }
  },
  "exp": 1673600917,
  "iat": 1673600857
}
```

### Tracking using the TrackingID from the MST
* If the `MST` has a `TrackingId`, then your `SST` will need to be set with the **same** `TrackingId`

#### Master Subscribe Token Example with Tracking information
```json
{
  "tokenId": 1,
  "token": "abcdefghijklmnopqrstuvwxyz012345678jh9",
  "streams": [
    {
        "streamName": "testStream"
    }
  ],
  "tracking": {
    "trackingId": "parentTrackingId"
  }
}
```

#### SST Example using the MST's TrackingID

```json
{
  "streaming": {
    "tokenId": 1,
    "tokenType": "Subscribe",
    "streamName": "teststream",
    "allowedOrigins": [],
    "allowedIpAddresses": [],
    "tracking": {
      "trackingId": "parentTrackingId"
    }
  },
  "exp": 1673600917,
  "iat": 1673600857
}
```