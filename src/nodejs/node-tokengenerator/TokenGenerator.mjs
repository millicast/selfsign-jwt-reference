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
   * @param {number} tokenId
   * @param {string} token
   * @param {string} streamName
   * @param {string[]=} allowedOrigins
   * @param {string[]=} allowedIpAddresses
   * @param {?Tracking} tracking
   * @param {?number} [expiresIn = 60]
   * @param {?string} customViewerData - Viewer data associated with connections using this token. Max length: 1024
   * @param {?string} originCluster
   * @returns {string}
   */
  createToken(tokenId, token, streamName,
              allowedOrigins, allowedIpAddresses,
              tracking = null,
              expiresIn = defaultExpiresIn,
              customViewerData = null,
              originCluster = null) {
    const streaming = {
      tokenId: tokenId,
      tokenType: 'Subscribe',
      streamName: streamName,
      originCluster: originCluster ?? null,
      tracking: tracking ?? null,
      customViewerData: customViewerData ?? null,
      allowedOrigins: allowedOrigins ?? [],
      allowedIpAddresses: allowedIpAddresses ?? []
    };
    const payload = {
      streaming: Object.fromEntries(Object.entries(streaming).filter(([_, v]) => v !== null))
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
