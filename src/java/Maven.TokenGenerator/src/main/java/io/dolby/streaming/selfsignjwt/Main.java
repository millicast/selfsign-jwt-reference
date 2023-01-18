package io.dolby.streaming.selfsignjwt;

import com.google.gson.Gson;
import io.dolby.streaming.models.SampleSubscribeToken;
import io.dolby.streaming.models.Tracking;

import java.io.*;
import java.nio.charset.StandardCharsets;

public class Main {

    private static Auth0TokenGenerator _tokenGenerator = new Auth0TokenGenerator();

    public static void main(String[] args) {

        // If there is no TrackingID on the Subscribe Token, we dont need to set one on the Self Signed Token
        var sstNoTrackingId = CreateSSTWithNoTrackingID();

        // If there is no TrackingID in the Subscribe Token, we can set a Custom TrackingID for a specific Self Signed Token
        var sstCustomTrackingId = CreateSSTWithCustomTrackingID();

        // If there is a TrackingID in the Subscribe Token, we need to set the same TrackingID on the Self Signed Token
        var sstParentTrackingId = CreateSSTWithParentTrackingID();

        System.out.println(
                "SST with no TrackingID : "+ sstNoTrackingId
                + "\nSST with custom TrackingID : "+ sstCustomTrackingId
                + "\nSST with parent TrackingID : "+ sstParentTrackingId);
    }


    private static String CreateSSTWithNoTrackingID(){
        var sampleToken = ParseJson("sampleSST.json");
        if (sampleToken == null) {
            System.exit(1);
            return null;
        }

        return _tokenGenerator.CreateToken(sampleToken.tokenId, sampleToken.token, sampleToken.streams.get(0).streamName, null);
        /*
        {
          "streaming": {
            "tokenId": 1,
            "tokenType": "Subscribe",
            "streamName": "testStream",
            "allowedOrigins": [],
            "allowedIpAddresses": [],
            "tracking": null
          },
          "exp": 1674016803
        }
         */
    }

    private static String CreateSSTWithCustomTrackingID(){
        // If there is no TrackingID in the Subscribe Token, then the SST can have any TrackingID you want.

        var sampleToken = ParseJson("sampleSSTWithNoParentTracking.json");
        if (sampleToken == null) {
            System.exit(1);
            return null;
        }

        return _tokenGenerator.CreateToken(sampleToken.tokenId, sampleToken.token, sampleToken.streams.get(0).streamName, new Tracking("SSTOnlyTrackingId"));
        /*
        {
            "streaming": {
            "tokenId": 1,
            "tokenType": "Subscribe",
            "streamName": "testStream",
            "allowedOrigins": [],
            "allowedIpAddresses": [],
            "tracking": {
              "trackingId": "SSTOnlyTrackingId"
            }
            },
            "exp": 1674020363
        }
         */
    }

    private static String CreateSSTWithParentTrackingID(){
        // If there is a TrackingID in the Subscribe Token, then the SST will need to have the TrackingID to be validated correctly when streaming.
        // This examples show this.
        var sampleToken = ParseJson("sampleSSTWithParentTracking.json");
        if (sampleToken == null) {
            System.exit(1);
            return null;
        }

        return _tokenGenerator.CreateToken(sampleToken.tokenId, sampleToken.token, sampleToken.streams.get(0).streamName, sampleToken.tracking);
        /*
        {
          "streaming": {
            "tokenId": 1,
            "tokenType": "Subscribe",
            "streamName": "testStream",
            "allowedOrigins": [],
            "allowedIpAddresses": [],
            "tracking": {
              "trackingId": "trackingIdFromParent"
            }
          },
          "exp": 1674018761
        }
         */
    }

    private static SampleSubscribeToken ParseJson(String sampleName) {
        var classloader = Thread.currentThread().getContextClassLoader();

        try (var stream = classloader.getResourceAsStream(sampleName);
             var reader = new InputStreamReader(stream, StandardCharsets.UTF_8);
             var buffer = new BufferedReader(reader)) {
            var gson = new Gson();
            return gson.fromJson(buffer, SampleSubscribeToken.class);
        } catch (Exception ex) {
            System.err.println("bad sample json");
            ex.printStackTrace();
            return null;
        }
    }
}
