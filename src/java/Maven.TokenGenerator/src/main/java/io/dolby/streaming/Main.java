package io.dolby.streaming;

import com.google.gson.Gson;
import java.io.*;
import java.nio.charset.StandardCharsets;

public class Main {
    public static void main(String[] args) {
        var sampleToken = ParseJson();
        if (sampleToken == null) {
            System.exit(1);
            return;
        }

        var generator = new Auth0TokenGenerator();
        // alternate library for JWT signing
        //var generator = new JavaJwtTokenGenerator();

        var selfSignToken = generator.CreateToken(sampleToken.tokenId, sampleToken.tokenString, sampleToken.streamName);

        System.out.println(selfSignToken);
    }

    private static SampleToken ParseJson() {
        var classloader = Thread.currentThread().getContextClassLoader();

        try (var stream = classloader.getResourceAsStream("sample.json");
             var reader = new InputStreamReader(stream, StandardCharsets.UTF_8);
             var buffer = new BufferedReader(reader)) {
            var gson = new Gson();
            return gson.fromJson(buffer, SampleToken.class);
        } catch (Exception ex) {
            System.err.println("bad sample json");
            ex.printStackTrace();
            return null;
        }
    }
}
