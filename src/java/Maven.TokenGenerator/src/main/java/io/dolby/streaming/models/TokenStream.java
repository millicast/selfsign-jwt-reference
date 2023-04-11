package io.dolby.streaming.models;

public class TokenStream {

    public String streamName;
    public Boolean isRegex;

    public TokenStream()
    {
        streamName = "";
        isRegex = false;
    }

    public TokenStream(String streamName, Boolean isRegex)
    {
        streamName = streamName;
        isRegex = isRegex;
    }

}