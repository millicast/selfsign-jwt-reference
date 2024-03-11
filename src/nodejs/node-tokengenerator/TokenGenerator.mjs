import JsonWebToken from 'jsonwebtoken';

export class Limits {
  static get customViewerData() {
    return 256;
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
   * @param {number} tokenId
   * @param {string} token
   * @param {string} streamName
   * @param {string[]=} allowedOrigins
   * @param {string[]=} allowedIpAddresses
   * @param {Tracking} tracking
   * @param {number} [expiresIn = 60]
   * @param {?string} customViewerData - Viewer data associated with connections using this token. Max length: 128
   * @returns {string}
   */
  createToken(tokenId, token, streamName,
              allowedOrigins, allowedIpAddresses ,
              tracking,
              expiresIn = 60,
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
      expiresIn: expiresIn
    };

    return JsonWebToken.sign(payload, token, signOptions);
  }

  _validateStreamingPayload(streaming) {
    if (streaming.customViewerData && streaming.customViewerData.length > Limits.customViewerData) {
      throw new Error(`customViewerData cannot be longer than: ${Limits.customViewerData}`)
    }
  }
}
