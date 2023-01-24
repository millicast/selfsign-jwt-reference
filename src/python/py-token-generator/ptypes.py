from typing import List


class Tracking:

    def __init__(self, tracking_id: str):
        self.trackingId = tracking_id


class SampleToken:
    """SampleToken type to hold data read from sample json files.
    """

    def __init__(self, id: int, token: str, streams: List[str], tracking: Tracking = None):
        self.id = id
        self.token = token
        self.streams = streams
        self.tracking = tracking
