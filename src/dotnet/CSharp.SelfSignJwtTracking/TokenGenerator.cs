namespace CSharp.SelfSignJwtTracking;

public class TokenGenerator
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly string _hmacAlg;

    public TokenGenerator()
        : this(SecurityAlgorithms.HmacSha256)
    {
    }

    private TokenGenerator(string hmacAlg)
    {
        _hmacAlg = hmacAlg;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public string CreateToken(uint tokenId, string tokenString, string streamName,
        Tracking? tracking = null,
        IEnumerable<string>? allowedOrigins = null, IEnumerable<string>? allowedIpAddresses = null,
        int expiresIn = 60,
        string? customViewerData = null)
    {
        var payload = new JwtPayload(tokenId, streamName, allowedOrigins, allowedIpAddresses, tracking, customViewerData);
        ValidateStreamingPayload(payload.streaming);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenString)), _hmacAlg),
            Claims = new Dictionary<string, object>()
            {
                {nameof(JwtPayload.streaming), payload.streaming}
            },
            Expires = DateTime.UtcNow.AddSeconds(expiresIn),
            NotBefore = null
        };
        var securityToken = _tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return _tokenHandler.WriteToken(securityToken);
    }

    void ValidateStreamingPayload(StreamPayload streaming)
    {
        if (streaming.customViewerData is { Length: > Limits.CustomViewerData })
        {
            throw new Exception($"customViewerData cannot be longer than: {Limits.CustomViewerData}");
        }
    }

    class JwtPayload
    {
        public StreamPayload streaming { get; }

        public JwtPayload(uint tokenId, string streamName, IEnumerable<string>? allowedOrigins, IEnumerable<string>? allowedIpAddresses, Tracking? tracking,
            string? customViewerData)
        {
            this.streaming = new StreamPayload(tokenId, streamName, allowedOrigins, allowedIpAddresses, tracking, customViewerData);
        }
    }

    class StreamPayload
    {
        public uint tokenId { get; }

        public string tokenType { get; } = "Subscribe";

        public string streamName { get; }

        public IEnumerable<string> allowedOrigins { get; }

        public IEnumerable<string> allowedIpAddresses { get; }
        
        public Tracking? tracking { get; }

        public string customViewerData { get; }

        public StreamPayload(uint tokenId, string streamName, IEnumerable<string>? allowedOrigins, IEnumerable<string>? allowedIpAddresses, Tracking? tracking,
            string? customViewerData)
        {
            this.tokenId = tokenId;
            this.streamName = streamName;
            this.allowedOrigins = allowedOrigins ?? Enumerable.Empty<string>();
            this.allowedIpAddresses = allowedIpAddresses ?? Enumerable.Empty<string>();
            this.tracking = tracking;
            this.customViewerData = customViewerData;
        }
    }
}

public static class Limits
{
    public const int CustomViewerData = 1024;
}