using System.Text.Json.Serialization;

namespace CSharp.SelfSignJwt;

public class SampleSubscribeToken
{
    [JsonPropertyName("id")]
    public uint tokenId { get; }
    public string token { get; }
    public List<TokenStream> streams { get; }
    public Tracking? tracking { get; }

    public SampleSubscribeToken()
    {
        this.streams = new List<TokenStream>();
        this.token = string.Empty;
    }
}

public class TokenStream
{
    public string streamName { get; }
    public bool isRegex { get; }

    public TokenStream()
    {
        this.streamName = string.Empty;
    }
}

public class Tracking
{
    public string trackingId { get; }

    public Tracking()
    {
        this.trackingId = string.Empty;
    }

    public Tracking(string trackingId)
    {
        this.trackingId = trackingId;
    }
}
