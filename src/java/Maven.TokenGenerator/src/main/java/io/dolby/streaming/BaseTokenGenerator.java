package io.dolby.streaming;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;

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

    public String CreateToken(long tokenId, String tokenString, String streamName) {
        return CreateToken(tokenId, tokenString, streamName, Collections.<String>emptyList(), Collections.<String>emptyList(), DefaultExpiresIn);
    }

    public String CreateToken(long tokenId, String tokenString, String streamName, int expiresIn) {
        return CreateToken(tokenId, tokenString, streamName, Collections.<String>emptyList(), Collections.<String>emptyList(), expiresIn);
    }

    public String CreateToken(long tokenId, String tokenString, String streamName, List<String> allowedOrigins, List<String> allowedIpAddresses) {
        return CreateToken(tokenId, tokenString, streamName, allowedOrigins, allowedIpAddresses, DefaultExpiresIn);
    }

    public abstract String CreateToken(long tokenId, String tokenString, String streamName, List<String> allowedOrigins, List<String> allowedIpAddresses, int expiresIn);

    protected static Map<String, ?> CreatePayload(long tokenId, String streamName, List<String> allowedOrigins, List<String> allowedIpAddresses) {
        var mapper = new ObjectMapper();

        var payload = new JwtPayload(tokenId, streamName, allowedOrigins, allowedIpAddresses);

        var typeRef = new TypeReference<Map<String, ?>>(){};
        return mapper.convertValue(payload, typeRef);
    }
}
