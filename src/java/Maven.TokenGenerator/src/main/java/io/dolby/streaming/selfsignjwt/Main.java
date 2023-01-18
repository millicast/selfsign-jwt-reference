package io.dolby.streaming.selfsignjwt;

import com.google.gson.Gson;
import io.dolby.streaming.models.SampleSubscribeToken;
import io.dolby.streaming.models.Tracking;

import java.io.*;
import java.nio.charset.StandardCharsets;

public class Main {
    public static void main(String[] args) {
        // If there is no TrackingID on the Subscribe Token, we dont need to set one on the Self Signed Token
        //CreateSSTWithNoTrackingID();

        // If there is no TrackingID in the Subscribe Token, we can set a Custom TrackingID for a specific Self Signed Token
        //CreateSSTWithCustomTrackingID();

        // If there is a TrackingID in the Subscribe Token, we need to set the same TrackingID on the Self Signed Token
        CreateSSTWithParentTrackingID();


    }


    private static void CreateSSTWithNoTrackingID(){
        var sampleToken = ParseJson("sampleSSTWithNoParentTracking.json");
        if (sampleToken == null) {
            System.exit(1);
            return;
        }

        var generator = new Auth0TokenGenerator();
        // alternate library for JWT signing
        //var generator = new JavaJwtTokenGenerator();

        //
        var selfSignToken = generator.CreateToken(sampleToken.tokenId, sampleToken.token, sampleToken.streams.get(0).streamName, null);

        System.out.println(selfSignToken);
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

    private static void CreateSSTWithCustomTrackingID(){
        // If there is no TrackingID in the Subscribe Token, then the SST can have any TrackingID you want.
        // This examples show this.
        var sampleToken = ParseJson("sampleSSTWithNoParentTracking.json");
        if (sampleToken == null) {
            System.exit(1);
            return;
        }

        var generator = new Auth0TokenGenerator();
        // alternate library for JWT signing
        //var generator = new JavaJwtTokenGenerator();

        //
        var selfSignToken = generator.CreateToken(sampleToken.tokenId, sampleToken.token, sampleToken.streams.get(0).streamName, new Tracking("SSTOnlyTrackingId"));

        System.out.println(selfSignToken);
    }

    private static void CreateSSTWithParentTrackingID(){
        // If there is a TrackingID in the Subscribe Token, then the SST will need to have the TrackingID to be validated correctly when streaming.
        // This examples show this.
        var sampleToken = ParseJson("sampleSSTWithParentTracking.json");
        if (sampleToken == null) {
            System.exit(1);
            return;
        }

        var generator = new Auth0TokenGenerator();
        // alternate library for JWT signing
        //var generator = new JavaJwtTokenGenerator();

        //
        var selfSignToken = generator.CreateToken(sampleToken.tokenId, sampleToken.token, sampleToken.streams.get(0).streamName, sampleToken.tracking);

        System.out.println(selfSignToken);
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
