package io.dolby.streaming.models;

import java.util.Collections;
import java.util.List;

public class StreamingPayload {
    public long tokenId;

    public String tokenType;

    public String streamName;

    public List<String> allowedOrigins;

    public List<String> allowedIpAddresses;

    public Tracking tracking;

    public StreamingPayload() {
        this.tokenType = "Subscribe";
    }

    public StreamingPayload(long tokenId, String streamName, List<String> allowedOrigins, List<String> allowedIpAddresses, Tracking tracking) {
        this();
        this.tokenId = tokenId;
        this.streamName = streamName;
        this.tracking = tracking;

        if (allowedOrigins == null) {
            allowedOrigins = Collections.<String>emptyList();
        }
        this.allowedOrigins = allowedOrigins;

        if (allowedIpAddresses == null) {
            allowedIpAddresses = Collections.<String>emptyList();
        }
        this.allowedIpAddresses = allowedIpAddresses;
    }
}
