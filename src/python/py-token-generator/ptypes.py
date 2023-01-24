import json


class Tracking:

    def __init__(self, trackingId: str):
        self.trackingId = trackingId


class SampleToken:
    """SampleToken type to hold data read from sample json files.
    """

    def __init__(self, id: int, token: str, streams: list[str], tracking: Tracking = None):
        self.id = id
        self.token = token
        self.streams = streams
        self.tracking = tracking
