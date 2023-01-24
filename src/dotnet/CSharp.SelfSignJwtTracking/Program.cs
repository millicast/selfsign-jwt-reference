
var tokenGenerator = new TokenGenerator();

/*
 * If there is no TrackingID on the Subscribe Token, we dont need to set one on the Self Signed Token if we don't want to.
 */

var sampleTokenWithNoParentTracking = JsonSerializer.Deserialize<SampleSubscribeToken>(File.OpenRead("../../../../../sample-json/sampleSST.json"));
if (sampleTokenWithNoParentTracking is null)
{
    throw new Exception("bad sample json");
}

var selfSignTokenWithNoTracking = tokenGenerator.CreateToken(sampleTokenWithNoParentTracking.tokenId,
    sampleTokenWithNoParentTracking.token,
    sampleTokenWithNoParentTracking.streams.First().streamName,
    sampleTokenWithNoParentTracking.tracking);

Console.WriteLine("SST With No Tracking enabled: "+ selfSignTokenWithNoTracking);

// Example JWT payload, when we don't set a TrackingID (and the Master Subscribe Token doesn't have Tracking)
/*
    {
  "streaming": {
    "tokenId": 373,
    "tokenType": "Subscribe",
    "streamName": "stream1",
    "allowedOrigins": [],
    "allowedIpAddresses": [],
    "tracking": {
      "trackingId": null
    }
  },
  "nbf": 1674521386,
  "exp": 1674521446,
  "iat": 1674521386
}
 */




/*
 * If there is Tracking already set on the Subscribe Token, then the SST must have the same TrackingID to as the Subscribe Token
 */

var sampleTokenWithParentTracking = JsonSerializer.Deserialize<SampleSubscribeToken>(File.OpenRead("../../../../../sample-json/sampleSSTWithParentTracking.json"));
if (sampleTokenWithParentTracking is null)
{
    throw new Exception("bad sample json");
}

var selfSignTokenWithParentTracking = tokenGenerator.CreateToken(sampleTokenWithParentTracking.tokenId,
    sampleTokenWithParentTracking.token,
    sampleTokenWithParentTracking.streams.First().streamName,
    sampleTokenWithParentTracking.tracking);

Console.WriteLine("SST With Parent Tracking: "+ selfSignTokenWithParentTracking);

// Example of JWT payload when created from a Subscribe Token which already has a TrackingID
/*
 * {
  "streaming": {
    "tokenId": 1,
    "tokenType": "Subscribe",
    "streamName": "testStream",
    "allowedOrigins": [],
    "allowedIpAddresses": [],
    "tracking": {
      "trackingId": "trackingIdFromParentToken"
    }
  },
  "nbf": 1673934423,
  "exp": 1673934483,
  "iat": 1673934423
}
 */


/* If there is no Tracking on the Subscribe Token, then we can set a custom TrackingID on the SST. */

var sampleTokenWithCustomTrackingId = JsonSerializer.Deserialize<SampleSubscribeToken>(File.OpenRead("../../../../../sample-json/sampleSSTWithNoParentTracking.json"));
if (sampleTokenWithCustomTrackingId is null)
{
    throw new Exception("bad sample json");
}

var customTrackingId = new Tracking("customTrackingId");
var selfSignTokenWithCustomTracking = tokenGenerator.CreateToken(sampleTokenWithCustomTrackingId.tokenId,
    sampleTokenWithCustomTrackingId.token,
    sampleTokenWithCustomTrackingId.streams.First().streamName, customTrackingId);

Console.WriteLine("SST with Custom TrackingID: " + selfSignTokenWithCustomTracking);

// Example of JWT payload when specifying custom TrackingID in the Self Signed token (where the master Subscribe Token does NOT have a TrackingID already)
/*
 * {
  "streaming": {
    "tokenId": 1,
    "tokenType": "Subscribe",
    "streamName": "testStream",
    "allowedOrigins": [],
    "allowedIpAddresses": [],
    "tracking": {
      "trackingId": "customTrackingId"
    }
  },
  "nbf": 1673934137,
  "exp": 1673934196,
  "iat": 1673934137
}
 */

