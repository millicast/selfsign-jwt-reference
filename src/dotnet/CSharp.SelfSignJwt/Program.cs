var tokenGenerator = new TokenGenerator();


var sampleToken = JsonSerializer.Deserialize<SampleSubscribeToken>(File.OpenRead("../../../../../sample-json/sampleSST.json"));
if (sampleToken is null)
{
    throw new Exception("bad sample json");
}

var streamName = sampleToken.streams.First().streamName;
var sstWithNoTrackingInformation = tokenGenerator.CreateToken(sampleToken.tokenId,
    sampleToken.token,
    streamName);
Console.WriteLine($"SST with no tracking information: {sstWithNoTrackingInformation}\n\n");



var sampleTokenWithParentTracking = JsonSerializer.Deserialize<SampleSubscribeToken>(File.OpenRead("../../../../../sample-json/sampleSSTWithParentTracking.json"));
if (sampleTokenWithParentTracking is null)
{
    throw new Exception("bad sample json");
}

streamName = ChooseStreamName(sampleTokenWithParentTracking);
var selfSignTokenWithParentTracking = tokenGenerator.CreateToken(sampleTokenWithParentTracking.tokenId,
    sampleTokenWithParentTracking.token,
    streamName,
    sampleTokenWithParentTracking.tracking);
Console.WriteLine($"SST With Parent Tracking: {selfSignTokenWithParentTracking}\n\n");



var sampleTokenWithCustomTrackingId = JsonSerializer.Deserialize<SampleSubscribeToken>(File.OpenRead("../../../../../sample-json/sampleSSTWithNoParentTracking.json"));
if (sampleTokenWithCustomTrackingId is null)
{
    throw new Exception("bad sample json");
}

streamName = ChooseStreamName(sampleTokenWithParentTracking);
var customTrackingId = new Tracking("customTrackingId2");
var selfSignTokenWithCustomTracking = tokenGenerator.CreateToken(sampleTokenWithCustomTrackingId.tokenId,
    sampleTokenWithCustomTrackingId.token,
    streamName,
    customTrackingId);
Console.WriteLine($"SST with Custom TrackingID: {selfSignTokenWithCustomTracking}\n\n");



var sampleTokenWithCustomViewerData = JsonSerializer.Deserialize<SampleSubscribeToken>(File.OpenRead("../../../../../sample-json/sampleSST.json"));
if (sampleTokenWithCustomViewerData is null)
{
    throw new Exception("bad sample json");
}

streamName = sampleTokenWithCustomViewerData.streams.First().streamName;
var selfSignTokenWithCustomViewerData = tokenGenerator.CreateToken(sampleTokenWithCustomViewerData.tokenId,
    sampleTokenWithCustomViewerData.token,
    streamName,
    customViewerData: "uniqueViewer1234");

Console.WriteLine($"SST With customViewerData: {selfSignTokenWithCustomViewerData}\n\n");


/***
 * For demo purposes if the sample token has more than 1 streamName, we will use the first non-regex StreamName to create SST.
 * If there are any Regexes in MST (example ".*"), then we need to specify an actual streamName to be used. This demo is only
 * supporting the ".*" global regex to generate a random stream name.
 */
string ChooseStreamName(SampleSubscribeToken subscribeToken)
{
    // choose one streamName from MST (that is not a regex) to include in the SST
    var testStream = subscribeToken.streams.FirstOrDefault(s => !s.isRegex)?.streamName;
    if (!string.IsNullOrEmpty(testStream))
    {
        return testStream;
    }
    // else implied the only streams defined are regex

    // for demo purposes we are only showing off choosing a random streamName from most obvious global ".*" regex
    if (!subscribeToken.streams.All(s => string.Equals(s.streamName, ".*")))
    {
        throw new NotImplementedException("not implemented other regexes");
    }

    // generate random streamName that will match ".*" regex
    return $"random_{Guid.NewGuid():N}";
}