using System.Text.Json.Serialization;

namespace CSharp.SelfSignJwt;

public class SampleSubscribeToken
{
    [JsonPropertyName("id")]
    public uint tokenId { get; init; }
    public string token { get; init; }
    public string originCluster { get; init; }
    public List<TokenStream> streams { get; init; }
    public Tracking? tracking { get; init; }

    public SampleSubscribeToken()
    {
        this.streams = new List<TokenStream>();
        this.token = string.Empty;
        this.originCluster = string.Empty;
    }
}

public class TokenStream
{
    public string streamName { get; init; }
    public bool isRegex { get; init; }

    public TokenStream()
    {
        this.streamName = string.Empty;
    }
}

public class Tracking
{
    public string trackingId { get; init; }

    public Tracking()
    {
        this.trackingId = string.Empty;
    }

    public Tracking(string trackingId)
    {
        this.trackingId = trackingId;
    }
}
