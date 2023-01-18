package io.dolby.streaming.models;

import java.util.ArrayList;

public class SampleSubscribeToken {
    public long tokenId;
    public String token;
    public ArrayList<TokenStream> streams;
    public Tracking tracking;

    public SampleSubscribeToken(){
        this.streams = new ArrayList<TokenStream>();
        this.tokenId = 0;
        this.token = "";
        this.tracking = new Tracking();

    }
}

//public class SampleSubscribeToken
//{
//    public uint tokenId { get; set; }
//    public string token { get; set; }
//    public List<TokenStream> streams { get; set; }
//    public Tracking tracking { get;set;}
//
//    public SampleSubscribeToken()
//    {
//        this.streams = new List<TokenStream>();
//        this.tokenId = default;
//        this.token = string.Empty;
//        this.tracking = new Tracking();
//    }
//}
//
//public class TokenStream
//{
//    public string streamName { get; set; }
//    public bool isRegex { get; set; }
//
//    public TokenStream()
//    {
//        streamName = string.Empty;
//        isRegex = default;
//    }
//
//    public TokenStream(string streamName, bool isRegex)
//    {
//        streamName = streamName;
//        isRegex = isRegex;
//    }
//}