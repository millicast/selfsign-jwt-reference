import JsonWebToken from 'jsonwebtoken';

const defaultExpiresIn = 60;

export class Limits {
  static get customViewerData() {
    return 1024;
  }
}

export default class TokenGenerator {
  /**
   *
   * @param {string} [hmacAlg = 'HS256']
   */
  constructor(hmacAlg = 'HS256') {
    this.hmacAlg = hmacAlg;
  }

  /**
   *
   * @param {number} tokenId - The tokenID of the master subscribe token
   * @param {string} token - The actual token string from the master subscribe token. This will be used to sign the Self Signed JWT.
   * @param {string} streamName - The stream name the SST is eligible for. This should be the stream name from the Master Subscribe Token. StreamNames are limited to 128 characters.
   * @param {string[]=} allowedOrigins - Optional. The Allowed Origins for the Self Signed JWT. Maximum of 10.
   * @param {string[]=} allowedIpAddresses - Optional. The allowed IP Addresses allowed for viewing with the Self Signed Token. Maximum of 25.
   * @param {Tracking} tracking - Optional when Master Subscribe Token does not have tracking. This helps track the SST and it's usage. TrackingId is limited to 128 characters.
   * @param {?number} [expiresIn = 60] - The expiry time for the Self Signed JWT.
   * @param {?string} customViewerData - Viewer data associated with connections using this token. Max length: 1024
   * @returns {string}
   */
  createToken(tokenId, token, streamName,
              allowedOrigins, allowedIpAddresses ,
              tracking,
              expiresIn = defaultExpiresIn,
              customViewerData = null) {
    const payload = {
      streaming: {
        tokenId: tokenId,
        tokenType: 'Subscribe',
        streamName: streamName,
        tracking: tracking ?? null,
        customViewerData: customViewerData ?? null,
        allowedOrigins: allowedOrigins ?? [],
        allowedIpAddresses: allowedIpAddresses ?? []
      }
    };
    this._validateStreamingPayload(payload.streaming);

    const signOptions = {
      algorithm: this.hmacAlg,
      expiresIn: expiresIn ?? defaultExpiresIn
    };

    return JsonWebToken.sign(payload, token, signOptions);
  }

  _validateStreamingPayload(streaming) {
    if (streaming.customViewerData && streaming.customViewerData.length > Limits.customViewerData) {
      throw new Error(`customViewerData cannot be longer than: ${Limits.customViewerData}`)
    }
  }
}
