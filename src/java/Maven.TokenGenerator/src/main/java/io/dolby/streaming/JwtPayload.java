package io.dolby.streaming;

import java.util.List;

public class JwtPayload {
    public StreamingPayload streaming;

    public JwtPayload(long tokenId, String streamName, List<String> allowedOrigins, List<String> allowedIpAddresses) {
        this.streaming = new StreamingPayload(tokenId, streamName, allowedOrigins, allowedIpAddresses);
    }
}
