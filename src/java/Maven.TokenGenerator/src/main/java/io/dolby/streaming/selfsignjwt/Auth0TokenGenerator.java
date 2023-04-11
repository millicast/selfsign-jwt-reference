package io.dolby.streaming.selfsignjwt;

import com.auth0.jwt.JWT;
import com.auth0.jwt.algorithms.Algorithm;
import io.dolby.streaming.models.Tracking;

import java.time.Instant;
import java.util.*;

public class Auth0TokenGenerator extends BaseTokenGenerator {
    public Auth0TokenGenerator() {
        super();
    }

    public Auth0TokenGenerator(String hmacAlg) {
        super(hmacAlg);
    }

    /**
     * Creates a Self Signed JWT using data found in the Master Subscribe Token
     * @param tokenId The tokenID of the master subscribe token
     * @param tokenString The actual token string from the master subscribe token. This will be used to sign the Self Signed JWT.
     * @param streamName The stream name the SST is eligible for. This should be the stream name from the Master Subscribe Token
     * @param allowedOrigins Optional. The Allowed Origins for the Self Signed JWT.
     * @param allowedIpAddresses Optional. The allowed IP Addresses allowed for viewing with the Self Signed Token.
     * @param expiresIn The expiry time for the Self Signed JWT.
     * @param tracking Optional when Master Subscribe Token does not have tracking. This helps track the SST and it's usage.
     * @return The Self Signed Token JWT.
     */
    public String createToken(long tokenId, String tokenString, String streamName, List<String> allowedOrigins, List<String> allowedIpAddresses, int expiresIn, Tracking tracking) {
        var payload = createPayload(tokenId, streamName, allowedOrigins, allowedIpAddresses, tracking);
        Algorithm algorithm = getAlgorithm(tokenString);
        return JWT.create()
                .withPayload(payload)
                .withExpiresAt(Instant.now().plusSeconds(expiresIn))
                .sign(algorithm);
    }

    /**
     * Chooses the Algorithm to sign the Self Signed Token with. We currently only support HMAC-256
     * @param tokenString The Master Subscribe Token's token string. This is used to sign the Self Signed Token.
     * @return The Algorithm used for signing the Self Signed Token.
     */
    private Algorithm getAlgorithm(String tokenString) {
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
