import jwt
from ptypes import Tracking
import datetime


class TokenGenerator:
    """Token Generator class.
    """

    def __init__(self, hmac_algorithm: str = 'HS256'):
        """Instantiate the token generator.

        :Parameter:
        - `hmac_algorithm`: The alhorithm to use to sign the token.
        """
        self.hmac_algorithm = hmac_algorithm

    def create_token(self, token_id: int, token: str, stream_name: str, tracking: Tracking = None,
                     allowed_origins: list = [], allowed_ip_addresses: list = [], expires_in: int = 60):
        """Create a token using the provided claims.

        :Parameters:
        - `token_id`: ID to be specified in the token.
        - `token_string`: Key to use in order to sign the token.
        - `stream_name`: Stream name to be specified in the token.
        - `allowed_origins`: Origins to be allowed for this token.
        - `allowed_ip_addresses`: Origins to be allowed for this token.
        - `tracking` : Tracking information
        - `expires_in`: Number of seconds before token expires.

        :Returns:
        - Signed JWT containing the claims provided as parameters.
        """
        payload = {
            'streaming': {
                'tokenId': token_id,
                'tokenType': 'Subscribe',
                'streamName': stream_name,
                'allowedOrigins': allowed_origins,
                'allowedIpAddresses': allowed_ip_addresses,
                'tracking': tracking
            },
            'exp': datetime.datetime.now(tz=datetime.timezone.utc) + datetime.timedelta(seconds=expires_in)
        }

        token = jwt.encode(payload, token, self.hmac_algorithm)
        return token
