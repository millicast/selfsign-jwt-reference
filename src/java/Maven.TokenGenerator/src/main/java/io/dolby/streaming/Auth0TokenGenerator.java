package io.dolby.streaming;

import com.auth0.jwt.JWT;
import com.auth0.jwt.algorithms.Algorithm;

import java.time.Instant;
import java.util.*;

public class Auth0TokenGenerator extends BaseTokenGenerator {
    public Auth0TokenGenerator() {
        super();
    }

    public Auth0TokenGenerator(String hmacAlg) {
        super(hmacAlg);
    }

    public String CreateToken(long tokenId, String tokenString, String streamName, List<String> allowedOrigins, List<String> allowedIpAddresses, int expiresIn) {
        var payload = CreatePayload(tokenId, streamName, allowedOrigins, allowedIpAddresses);
        Algorithm algorithm = GetAlgorithm(tokenString);
        return JWT.create()
                .withPayload(payload)
                .withExpiresAt(Instant.now().plusSeconds(expiresIn))
                .sign(algorithm);
    }

    private Algorithm GetAlgorithm(String tokenString) {
        switch (_hmacAlg) {
            case "HS256":
                return Algorithm.HMAC256(tokenString);
            case "HS384":
                return Algorithm.HMAC384(tokenString);
            case "HS512":
                return Algorithm.HMAC512(tokenString);
            default:
                throw new RuntimeException("unsupported HMAC algorithm: " + _hmacAlg);
        }
    }
}
