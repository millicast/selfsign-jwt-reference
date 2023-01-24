package io.dolby.streaming.models;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class SampleSubscribeToken {
    @SerializedName("id")
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