import json
from ptypes import SampleToken
from ptypes import Tracking
from token_generator import TokenGenerator


def get_sample(filename: str):
    with open(filename) as f:
        sample = json.loads(f.read())
        if not sample:
            raise Exception('Bad sample json')
        return SampleToken(**sample)


def main():
    generator = TokenGenerator()

    # If there is no TrackingID on the Subscribe Token, we don't need to set one on the Self Signed Token
    no_tracking_sample = get_sample('../../sample-json/sampleSST.json')
    sst_with_no_tracking_info = generator.create_token(no_tracking_sample.id,
                                                       no_tracking_sample.token,
                                                       no_tracking_sample.streams[0]['streamName'],
                                                       no_tracking_sample.tracking)

    # If there is a TrackingID in the Subscribe Token, we need to set the same TrackingID on the Self Signed Token
    parent_tracking_sample = get_sample('../../sample-json/sampleSSTWithParentTracking.json')
    sst_with_parent_tracking_info = generator.create_token(parent_tracking_sample.id,
                                                           parent_tracking_sample.token,
                                                           parent_tracking_sample.streams[0]['streamName'],
                                                           parent_tracking_sample.tracking)

    # If there is no TrackingID in the Subscribe Token, we can set a Custom TrackingID for a specific Self Signed Token
    custom_tracking_sample = get_sample('../../sample-json/sampleSSTWithNoParentTracking.json')
    custom_tracking_id = Tracking("custom_tracking_id")
    sst_with_custom_tracking_info = generator.create_token(custom_tracking_sample.id,
                                                           custom_tracking_sample.token,
                                                           custom_tracking_sample.streams[0]['streamName'],
                                                           custom_tracking_id.__dict__)

    print(f"\n"
          f"    SST with no TrackingID: {sst_with_no_tracking_info}\n"
          f"    SST with parent TrackingID: {sst_with_parent_tracking_info}\n"
          f"    SST with custom TrackingID: {sst_with_custom_tracking_info}\n"
          f"    ")


if '__main__' == __name__:
    main()
