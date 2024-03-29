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
        var sstNoTrackingId = createSSTWithNoTrackingID();

        // If there is no TrackingID in the Subscribe Token, we can set a Custom TrackingID for a specific Self Signed Token
        var sstCustomTrackingId = createSSTWithCustomTrackingID();

        // If there is a TrackingID in the Subscribe Token, we need to set the same TrackingID on the Self Signed Token
        var sstParentTrackingId = createSSTWithParentTrackingID();

        System.out.println(
                "SST with no TrackingID : "+ sstNoTrackingId +
                "\nSST with custom TrackingID : "+ sstCustomTrackingId +
                "\nSST with parent TrackingID : "+ sstParentTrackingId);
    }


    /**
     * If there is no TrackingID in the Subscribe Token, we don't have to set a TrackingID in the SST if we don't want to.
     * @return An example JWT with no TrackingID.
     */
    private static String createSSTWithNoTrackingID(){
        var sampleToken = parseJson("../../sample-json/sampleSST.json");
        if (sampleToken == null) {
            System.exit(1);
            return null;
        }
        var streamName = "";
        // If the MST has streamNames, then the SST streamName has to match with atleast one in MST streamNames.
        // If there's only Regex in there (so global ".*"), then we need to specify an actual streamName to be used

        if (sampleToken.streams.size() == 1 &&
                sampleToken.streams.get(0).streamName.equals(".*"))
        {
            // if there's only one streamName in MST, and it's global then we have to set the streamName ourselves
            streamName = "lfbt28hq";
        }
        else
        {
            // choose one streamName from MST (that's not the global .*) to include in the SST
            sampleToken.streams.removeIf(c -> c.streamName.equals(".*"));
            streamName = sampleToken.streams.get(0).streamName;
        }


        return _tokenGenerator.createToken(sampleToken.tokenId, sampleToken.token, streamName, sampleToken.tracking);
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

    /**
     * If there is no TrackingID in the Subscribe Token, then the SST can have any TrackingID you want.
     * @return An example JWT with a custom TrackingID.
     */
    private static String createSSTWithCustomTrackingID(){
        // If there is no TrackingID in the Subscribe Token, then the SST can have any TrackingID you want.

        var sampleToken = parseJson("../../sample-json/sampleSSTWithNoParentTracking.json");
        if (sampleToken == null) {
            System.exit(1);
            return null;
        }

        var streamName = "";
        // If the MST has streamNames, then the SST streamName has to match with atleast one in MST streamNames.
        // If there's only Regex in there (so global ".*"), then we need to specify an actual streamName to be used. This can be custom

        if (sampleToken.streams.size() == 1 &&
                sampleToken.streams.get(0).streamName.equals(".*"))
        {
            // if there's only one streamName in MST, and it's global then we have to set the streamName ourselves
            streamName = "lfbt28hq";
        }
        else
        {
            // choose one streamName from MST (that's not the global .*) to include in the SST
            sampleToken.streams.removeIf(c -> c.streamName.equals(".*"));
            streamName = sampleToken.streams.get(0).streamName;
        }

        return _tokenGenerator.createToken(sampleToken.tokenId, sampleToken.token, streamName, new Tracking("SSTOnlyTrackingId"));
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

    /**
     * If there is a TrackingID in the Subscribe Token, then the SST will need to have the TrackingID to be validated correctly when viewing a stream.
     * @return An example JWT with the TrackingID the same as the parent.
     */
    private static String createSSTWithParentTrackingID(){
        var sampleToken = parseJson("../../sample-json/sampleSSTWithParentTracking.json");
        if (sampleToken == null) {
            System.exit(1);
            return null;
        }

        var streamName = "";
        // If the MST has streamNames, then the SST streamName has to match with atleast one in MST streamNames.
        // If there's only Regex in there (so global ".*"), then we need to specify an actual streamName to be used. This can be custom

        if (sampleToken.streams.size() == 1 &&
                sampleToken.streams.get(0).streamName.equals(".*"))
        {
            // if there's only one streamName in MST, and it's global then we have to set the streamName ourselves
            streamName = "lfbt28hq";
        }
        else
        {
            // choose one streamName from MST (that's not the global .*) to include in the SST
            sampleToken.streams.removeIf(c -> c.streamName.equals(".*"));
            streamName = sampleToken.streams.get(0).streamName;
        }

        return _tokenGenerator.createToken(sampleToken.tokenId, sampleToken.token, streamName, sampleToken.tracking);
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

    /**
     * If there is a TrackingID in the Subscribe Token, then the SST will need to have the TrackingID to be validated correctly when viewing a stream.
     * @param sampleName The JSON file with the master subscribe token
     * @return A model representing the master subscribe token.
     */
    private static SampleSubscribeToken parseJson(String sampleName) {
        try (var stream = new FileInputStream(sampleName);
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
