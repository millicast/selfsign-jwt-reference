
var tokenGenerator = new TokenGenerator();
var streamName = string.Empty;
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

// If the MST has streamNames, then the SST streamName has to match with atleast one in MST streamNames.
// If there's only Regex in there (so global ".*"), then we need to specify an actual streamName to be used

if (sampleTokenWithParentTracking.streams.Count == 1 &&
    sampleTokenWithParentTracking.streams.First().streamName == ".*")
{
    // if there's only one streamName in MST and it's global then we have to set the streamName ourselves
    streamName = "lfbt28hq";
}
else
{
    // choose one streamName from MST (that's not the global .*) to include in the SST
    streamName = sampleTokenWithParentTracking.streams.First(c => c.streamName != ".*").streamName;
}

var selfSignTokenWithParentTracking = tokenGenerator.CreateToken(sampleTokenWithParentTracking.tokenId,
    sampleTokenWithParentTracking.token,
    streamName,
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

// If the MST has streamNames, then the SST streamName has to match with atleast one in MST streamNames.
// If there's only Regex in there (so global ".*"), then we need to specify an actual streamName to be used

if (sampleTokenWithCustomTrackingId.streams.Count == 1 &&
    sampleTokenWithCustomTrackingId.streams.First().streamName == ".*")
{
    // if there's only one streamName in MST and it's global then we have to set the streamName ourselves
    streamName = "lfbt28hq";
}
else
{
    // choose one streamName from MST (that's not the global .*) to include in the SST
    streamName = sampleTokenWithCustomTrackingId.streams.First(c => c.streamName != ".*").streamName;
}
var customTrackingId = new Tracking("customTrackingId2");
var selfSignTokenWithCustomTracking = tokenGenerator.CreateToken(sampleTokenWithCustomTrackingId.tokenId,
    sampleTokenWithCustomTrackingId.token,
    streamName, customTrackingId);

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



var sampleTokenWithCustomViewerData = JsonSerializer.Deserialize<SampleSubscribeToken>(File.OpenRead("../../../../../sample-json/sampleSST.json"));
if (sampleTokenWithCustomViewerData is null)
{
    throw new Exception("bad sample json");
}

var selfSignTokenWithCustomViewerData = tokenGenerator.CreateToken(sampleTokenWithCustomViewerData.tokenId,
    sampleTokenWithCustomViewerData.token,
    sampleTokenWithCustomViewerData.streams.First().streamName,
    customViewerData: "uniqueViewer1234");

Console.WriteLine("SST With customViewerData: "+ selfSignTokenWithCustomViewerData);

// Example JWT payload, when we don't set a TrackingID (and the Master Subscribe Token doesn't have Tracking)
/*
 * {
  "streaming": {
    "tokenId": 1,
    "tokenType": "Subscribe",
    "streamName": "testStream",
    "allowedOrigins": [],
    "allowedIpAddresses": [],
    "tracking": null,
    "customViewerData": "uniqueViewer1234"
  },
  "nbf": 1673934423,
  "exp": 1673934483,
  "iat": 1673934423
}
 */