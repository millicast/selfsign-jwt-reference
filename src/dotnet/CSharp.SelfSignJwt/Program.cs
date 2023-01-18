var sampleToken = JsonSerializer.Deserialize<SampleSubscribeToken>(File.OpenRead("sampleSelfSigned.json"));
if (sampleToken is null)
{
    throw new Exception("bad sample json");
}

var tokenGenerator = new TokenGenerator();
var selfSignToken = tokenGenerator.CreateToken(sampleToken.tokenId, sampleToken.token, sampleToken.streams.First().streamName);

Console.WriteLine(selfSignToken);
