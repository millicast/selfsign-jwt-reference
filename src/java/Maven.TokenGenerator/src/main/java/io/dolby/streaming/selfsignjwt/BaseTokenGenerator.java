package io.dolby.streaming.selfsignjwt;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import io.dolby.streaming.models.JwtPayload;
import io.dolby.streaming.models.Tracking;

import java.util.Collections;
import java.util.List;
import java.util.Map;

public abstract class BaseTokenGenerator {
    protected static final int DefaultExpiresIn = 60;
    protected static final String DefaultHmacAlg = "HS256";

    protected String _hmacAlg;

    public BaseTokenGenerator() {
        this(DefaultHmacAlg);
    }

    public BaseTokenGenerator(String hmacAlg) {
        _hmacAlg = hmacAlg;
    }

    public String createToken(long tokenId, String tokenString, String streamName, Tracking tracking) {
        return createToken(tokenId, tokenString, streamName, Collections.<String>emptyList(), Collections.<String>emptyList(), DefaultExpiresIn, tracking);
    }

    public String createToken(long tokenId, String tokenString, String streamName, int expiresIn, Tracking tracking) {
        return createToken(tokenId, tokenString, streamName, Collections.<String>emptyList(), Collections.<String>emptyList(), expiresIn, tracking);
    }

    public String createToken(long tokenId, String tokenString, String streamName, List<String> allowedOrigins, List<String> allowedIpAddresses, Tracking tracking) {
        return createToken(tokenId, tokenString, streamName, allowedOrigins, allowedIpAddresses, DefaultExpiresIn, tracking);
    }

    public abstract String createToken(long tokenId, String tokenString, String streamName, List<String> allowedOrigins, List<String> allowedIpAddresses, int expiresIn, Tracking tracking);

    protected static Map<String, ?> createPayload(long tokenId, String streamName, List<String> allowedOrigins, List<String> allowedIpAddresses, Tracking tracking) {
        var mapper = new ObjectMapper();

        var payload = new JwtPayload(tokenId, streamName, allowedOrigins, allowedIpAddresses, tracking);

        var typeRef = new TypeReference<Map<String, ?>>(){};
        return mapper.convertValue(payload, typeRef);
    }
}
