var sampleToken = JsonSerializer.Deserialize<SampleSubscribeToken>(File.OpenRead("../../../../../sample-json/sampleSST.json"));
if (sampleToken is null)
{
    throw new Exception("bad sample json");
}

var tokenGenerator = new TokenGenerator();
var sstWithNoTrackingInformation = tokenGenerator.CreateToken(sampleToken.tokenId, sampleToken.token, sampleToken.streams.First().streamName);

Console.WriteLine("SST with no tracking information: " + sstWithNoTrackingInformation);
