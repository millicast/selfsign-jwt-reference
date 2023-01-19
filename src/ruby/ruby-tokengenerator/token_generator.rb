require 'jwt'

class TokenGenerator
  def initialize(hmac_alg = "HS256")
    @_hmac_alg = hmac_alg
  end

  def create_token(token_id, token_string, stream_name, allowed_origins = nil, allowed_ip_addresses = nil, tracking = nil, expires_in = 60)
    allowed_origins ||= []
    allowed_ip_addresses ||= []

    header = {
      typ: 'JWT'
    }

    payload = {
      streaming: {
        tokenId: token_id,
        tokenType: 'Subscribe',
        streamName: stream_name,
        allowedOrigins: allowed_origins,
        allowedIpAddresses: allowed_ip_addresses,
        tracking: tracking
      },
      exp: Time.now.to_i + expires_in
    }

    JWT.encode(payload, token_string, algorithm=@_hmac_alg, header_fields=header)
  end
end
