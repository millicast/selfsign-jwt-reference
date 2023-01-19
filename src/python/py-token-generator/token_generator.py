
import jwt

class TokenGenerator:
    '''Token Generator class.
    '''

    def __init__(self, hmac_algorithm:str='HS256'):
        """Instantiate the token generator.

        :Parameter:
        - `hmac_algorithm`: The alhorithm to use to sign the token.
        """
        self.hmac_algorithm = hmac_algorithm

    def create_token(self, token_id:int, token_string:str, stream_name:str, allowed_origins:list=[], allowed_ip_addresses:list=[]):
        """Create a token using the provided claims.

        :Parameters:
        - `token_id`: ID to be specified in the token.
        - `token_string`: Key to use in order to sign the token.
        - `stream_name`: Stream name to be specified in the token.
        - `allowed_origins`: Origins to be allowed for this token.
        - `allowed_ip_addresses`: Origins to be allowed for this token.

        :Returns:
        - Signed JWT containing the claims provided as parameters.
        """
        payload = {
            'streaming': {
                'tokenId': token_id,
                'tokenType': 'Subscribe',
                'streamName': stream_name,
                'allowedOrigins': allowed_origins,
                'allowedIpAddresses': allowed_ip_addresses
            }
        }

        token = jwt.encode(payload, token_string, self.hmac_algorithm)
        return token
