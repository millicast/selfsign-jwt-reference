package io.dolby.streaming.selfsignjwt;

import io.dolby.streaming.models.Tracking;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;
import io.jsonwebtoken.security.Keys;

import java.nio.charset.StandardCharsets;
import java.util.Date;
import java.util.List;

public class JavaJwtTokenGenerator extends BaseTokenGenerator {
    public JavaJwtTokenGenerator() {
        super();
    }

    public JavaJwtTokenGenerator(String hmacAlg) {
        super(hmacAlg);
    }

    public String createToken(long tokenId, String tokenString, String streamName, List<String> allowedOrigins, List<String> allowedIpAddresses, int expiresIn, Tracking tracking) {
        var secretKey = Keys.hmacShaKeyFor(tokenString.getBytes(StandardCharsets.US_ASCII));

        var now = new Date();
        now.setTime(now.getTime() + expiresIn);

        var payload = createPayload(tokenId, streamName, allowedOrigins, allowedIpAddresses, tracking);

        var builder = Jwts.builder()
                .setHeaderParam("typ", "JWT")
                .setClaims(payload)
                .setExpiration(now)
                .signWith(secretKey, getSignatureAlgorithm());

        return builder.compact();
    }

    private SignatureAlgorithm getSignatureAlgorithm() {
        switch (_hmacAlg) {
            case "HS256":
                return SignatureAlgorithm.HS256;
            case "HS384":
                return SignatureAlgorithm.HS384;
            case "HS512":
                return SignatureAlgorithm.HS512;
            default:
                throw new RuntimeException("unsupported HMAC algorithm: " + _hmacAlg);
        }
    }
}
