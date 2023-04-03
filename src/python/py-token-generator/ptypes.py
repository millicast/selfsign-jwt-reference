from typing import List
from datetime import datetime

class Tracking:

    def __init__(self, tracking_id: str):
        self.trackingId = tracking_id


class SampleToken:
    """SampleToken type to hold data read from sample json files.
    """

    def __init__(self, id: int, token: str, streams: List[str],
                 allowedOrigins: List[str], originCluster: str, label: str,
                 allowedIpAddresses: List[str], allowedCountries: List[str], deniedCountries: List[str],
                 addedOn: datetime, isActive: bool, tracking: Tracking = None):
        self.id = id
        self.token = token
        self.streams = streams
        self.tracking = tracking
        self.originCluster = originCluster
        self.label = label
        self.addedOn = addedOn
        self.isActive = isActive
        self.allowedOrigins = allowedOrigins
        self.allowedIpAddresses = allowedIpAddresses
        self.allowedCountries = allowedCountries
        self.deniedCountries = deniedCountries
