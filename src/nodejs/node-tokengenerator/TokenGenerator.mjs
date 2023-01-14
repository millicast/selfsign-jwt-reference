import JsonWebToken from 'jsonwebtoken';

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
   * @param {string} tokenString
   * @param {string} streamName
   * @param {string[]=} allowedOrigins
   * @param {string[]=} allowedIpAddresses
   * @param {number} [expiresIn = 60]
   * @returns {string}
   */
  createToken(tokenId, tokenString, streamName, allowedOrigins, allowedIpAddresses, expiresIn = 60) {
    const payload = {
      streaming: {
        tokenId: tokenId,
        tokenType: 'Subscribe',
        streamName: streamName,
        allowedOrigins: allowedOrigins ?? [],
        allowedIpAddresses: allowedIpAddresses ?? []
      }
    };

    const signOptions = {
      algorithm: this.hmacAlg,
      expiresIn: expiresIn
    };

    return JsonWebToken.sign(payload, tokenString, signOptions);
  }
}
