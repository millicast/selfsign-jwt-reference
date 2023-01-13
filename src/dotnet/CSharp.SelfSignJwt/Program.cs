var sampleToken = JsonSerializer.Deserialize<SampleToken>(File.OpenRead("sample.json"));
if (sampleToken is null)
{
    throw new Exception("bad sample json");
}

var tokenGenerator = new TokenGenerator();
var selfSignToken = tokenGenerator.CreateToken(sampleToken.tokenId, sampleToken.tokenString, sampleToken.streamName);

Console.WriteLine(selfSignToken);
