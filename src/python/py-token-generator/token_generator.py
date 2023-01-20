import datetime
import jwt


class TokenGenerator:
    """Token Generator class.
    """

    def __init__(self, hmac_algorithm: str = 'HS256'):
        """Instantiate the token generator.

        :Parameter:
        - `hmac_algorithm`: The algorithm to use to sign the token.
        """
        self.hmac_algorithm = hmac_algorithm

    def create_token(self, token_id: int, token_string: str, stream_name: str,
                     allowed_origins: list = None, allowed_ip_addresses: list = None,
                     expires_in: int = 60):
        """Create a token using the provided claims.

        :Parameters:
        - `token_id`: ID to be specified in the token.
        - `token_string`: Key to use in order to sign the token.
        - `stream_name`: Stream name to be specified in the token.
        - `allowed_origins`: Origins to be allowed for this token.
        - `allowed_ip_addresses`: Origins to be allowed for this token.
        - `expires_in`: Number of seconds before token expires.

        :Returns:
        - Signed JWT containing the claims provided as parameters.
        """

        if allowed_origins is None:
            allowed_origins = []
        if allowed_ip_addresses is None:
            allowed_ip_addresses = []

        now_utc = datetime.datetime.now(tz=datetime.timezone.utc)
        payload = {
            'streaming': {
                'tokenId': token_id,
                'tokenType': 'Subscribe',
                'streamName': stream_name,
                'allowedOrigins': allowed_origins,
                'allowedIpAddresses': allowed_ip_addresses
            },
            'iat': now_utc,
            'exp': now_utc + datetime.timedelta(seconds=expires_in)
        }

        token = jwt.encode(payload, token_string, algorithm=self.hmac_algorithm)
        return token.decode()
