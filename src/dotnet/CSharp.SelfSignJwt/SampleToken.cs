namespace CSharp.SelfSignJwt;

public class SampleToken
{
    public uint tokenId { get; set; }
    public string tokenString { get; set; }
    public string streamName { get; set; }

    public SampleToken()
    {
        this.tokenId = default;
        this.tokenString = this.streamName = string.Empty;
    }
}