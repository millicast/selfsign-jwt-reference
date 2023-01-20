import json


class Tracking:

    def __init__(self, trackingId: str):
        self.trackingId = trackingId


class SampleToken:
    """SampleToken type to hold data read from sample json files.
    """

    def __init__(self, tokenId: int, token: str, streams: list[str], tracking: Tracking = None):
        self.token_id = tokenId
        self.token = token
        self.streams = streams
        self.tracking = tracking
