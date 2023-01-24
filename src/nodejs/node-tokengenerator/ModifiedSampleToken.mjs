export default class ModifiedSampleToken{
    constructor(tokenId, token, streams, allowedOrigins, allowedIpAddresses, tracking) {
        this.id = tokenId;
        this.token = token;
        this.streams = streams;
        this.allowedOrigins = allowedOrigins;
        this.allowedIpAddresses = allowedIpAddresses;
        this.tracking = tracking;
    }

    fromJson(){
        let json = JSON.stringify(this);
        return JSON.parse(json);
    }
}