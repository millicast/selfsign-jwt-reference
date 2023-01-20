// If there is Tracking already set on the Subscribe Token, then the SST must have the same TrackingID to as the Subscribe Token

var sampleTokenWithParentTracking = JsonSerializer.Deserialize<SampleSubscribeToken>(File.OpenRead("../../../../../sample-json/sampleSSTWithParentTracking.json"));
if (sampleTokenWithParentTracking is null)
{
    throw new Exception("bad sample json");
}
var tokenGenerator = new TokenGenerator();
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
 * 
 */


// --------------------------------


/* If there is not Tracking on the Subscribe Token, then it can be set on the SST. */

var sampleTokenWithNoParentTracking = JsonSerializer.Deserialize<SampleSubscribeToken>(File.OpenRead("../../../../../sample-json/sampleSSTWithNoParentTracking.json"));
if (sampleTokenWithNoParentTracking is null)
{
    throw new Exception("bad sample json");
}

var customTrackingId = new Tracking("customTrackingId");
var selfSignTokenWithCustomTracking = tokenGenerator.CreateToken(sampleTokenWithNoParentTracking.tokenId,
    sampleTokenWithNoParentTracking.token,
    sampleTokenWithNoParentTracking.streams.First().streamName, customTrackingId);

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

