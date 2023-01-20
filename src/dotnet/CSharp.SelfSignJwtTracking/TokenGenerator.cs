namespace CSharp.SelfSignJwtTracking;

public class TokenGenerator
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly string _hmacAlg;

    public TokenGenerator()
        : this(SecurityAlgorithms.HmacSha256)
    {
    }

    public TokenGenerator(string hmacAlg)
    {
        _hmacAlg = hmacAlg;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public string CreateToken(uint tokenId, string tokenString, string streamName, Tracking tracking,
        IEnumerable<string>? allowedOrigins = null, IEnumerable<string>? allowedIpAddresses = null,
        int expiresIn = 60)
    {
        // // Example for specifying custom TrackingID in the Self Signed Token
        // if (tracking == null)
        // {
        //     tracking = new Tracking("customSelfSignedTrackingId");
        // }
        var payload = new JwtPayload(tokenId, streamName, allowedOrigins, allowedIpAddresses, tracking);

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

    class JwtPayload
    {
        public StreamPayload streaming { get; }

        public JwtPayload(uint tokenId, string streamName, IEnumerable<string>? allowedOrigins, IEnumerable<string>? allowedIpAddresses, Tracking tracking)
        {
            this.streaming = new StreamPayload(tokenId, streamName, allowedOrigins, allowedIpAddresses, tracking);
        }
    }

    class StreamPayload
    {
        public uint tokenId { get; }

        public string tokenType { get; } = "Subscribe";

        public string streamName { get; }

        public IEnumerable<string> allowedOrigins { get; }

        public IEnumerable<string> allowedIpAddresses { get; }
        
        public Tracking tracking { get; }

        public StreamPayload(uint tokenId, string streamName, IEnumerable<string>? allowedOrigins, IEnumerable<string>? allowedIpAddresses, Tracking tracking)
        {
            this.tokenId = tokenId;
            this.streamName = streamName;
            this.allowedOrigins = allowedOrigins ?? Enumerable.Empty<string>();
            this.allowedIpAddresses = allowedIpAddresses ?? Enumerable.Empty<string>();
            this.tracking = tracking;
        }
    }
}