
class SampleToken:
    '''SampleToken type to hold data read from `sample.json` file.
    '''

    def __init__(self, tokenId:int, tokenString:str, streamName:str):
        self.token_id = tokenId
        self.token_string = tokenString
        self.stream_name = streamName
