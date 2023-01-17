using System.Diagnostics;

namespace CSharp.SelfSignJwtTracking;

public class SampleSubscribeToken
{
    public uint tokenId { get; set; }
    public string token { get; set; }
    public List<TokenStream> streams { get; set; }
    public Tracking tracking { get;set;}

    public SampleSubscribeToken()
    {
        this.streams = new List<TokenStream>();
        this.tokenId = default;
        this.token = string.Empty;
        this.tracking = new Tracking();
    }
}

public class TokenStream
{
    public string streamName { get; set; }
    public bool isRegex { get; set; }

    public TokenStream()
    {
        streamName = string.Empty;
        isRegex = default;
    }

    public TokenStream(string streamName, bool isRegex)
    {
        streamName = streamName;
        isRegex = isRegex;
    }
}