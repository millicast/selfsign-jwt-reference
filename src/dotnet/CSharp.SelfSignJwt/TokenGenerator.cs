using System.Text.Json.Serialization;

namespace CSharp.SelfSignJwt;

public class TokenGenerator
{
    private static readonly JsonSerializerOptions s_serializerOptions;

    static TokenGenerator()
    {
        s_serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General)
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
    }

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

    public string CreateToken(uint tokenId, string tokenString, string streamName,
        Tracking? tracking = null,
        IEnumerable<string>? allowedOrigins = null, IEnumerable<string>? allowedIpAddresses = null,
        int expiresIn = 60,
        string? customViewerData = null,
        string? originCluster = null)
    {
        var payload = new JwtPayload(tokenId, streamName, allowedOrigins, allowedIpAddresses, tracking,
            customViewerData, originCluster);
        ValidateStreamingPayload(payload.streaming);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenString)), _hmacAlg),
            Claims = new Dictionary<string, object>()
            {
                {nameof(JwtPayload.streaming), JsonSerializer.SerializeToElement(payload.streaming, s_serializerOptions)}
            },
            Expires = DateTime.UtcNow.AddSeconds(expiresIn)
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

        public JwtPayload(uint tokenId, string streamName,
            IEnumerable<string>? allowedOrigins, IEnumerable<string>? allowedIpAddresses, Tracking? tracking,
            string? customViewerData,
            string? originCluster)
        {
            this.streaming = new StreamPayload(tokenId, streamName,
                allowedOrigins, allowedIpAddresses, tracking,
                customViewerData, originCluster);
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

        public string? customViewerData { get; }

        public string? originCluster { get; }

        public StreamPayload(uint tokenId, string streamName,
            IEnumerable<string>? allowedOrigins, IEnumerable<string>? allowedIpAddresses, Tracking? tracking,
            string? customViewerData,
            string? originCluster)
        {
            this.tokenId = tokenId;
            this.streamName = streamName;
            this.allowedOrigins = allowedOrigins ?? Enumerable.Empty<string>();
            this.allowedIpAddresses = allowedIpAddresses ?? Enumerable.Empty<string>();
            this.tracking = tracking;
            this.customViewerData = customViewerData;
            this.originCluster = originCluster;
        }
    }
}

public static class Limits
{
    public const int CustomViewerData = 1024;
}