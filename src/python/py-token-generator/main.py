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


def with_no_tracking_info(generator: TokenGenerator):
    no_tracking_sample = get_sample('../../sample-json/sampleSST.json')

    # If the MST has streamNames, then the SST streamName has to match with atleast one in MST streamNames.
    # If there's only Regex in there (so global ".*"), then we need to specify an actual streamName to be used

    if len(no_tracking_sample.streams) == 1 and no_tracking_sample.streams[0]['streamName'] == '.*':
        # if there's only one streamName in MST, and it's global then we have to set the streamName ourselves
        stream_name = "customStreamName"
    else:
        # choose one streamName from MST (that's not the global .*) to include in the SST
        no_tracking_sample.streams = [i for i in no_tracking_sample.streams if i['streamName'] != '.*']
        stream_name = no_tracking_sample.streams[0]['streamName']

    sst_with_no_tracking_info = generator.create_token(no_tracking_sample.id,
                                                       no_tracking_sample.token,
                                                       stream_name,
                                                       no_tracking_sample.tracking)
    return sst_with_no_tracking_info


def with_parent_tracking(generator: TokenGenerator):
    parent_tracking_sample = get_sample('../../sample-json/sampleSSTWithParentTracking.json')

    # If the MST has streamNames, then the SST streamName has to match with atleast one in MST streamNames.
    # If there's only Regex in there (so global ".*"), then we need to specify an actual streamName to be used

    if len(parent_tracking_sample.streams) == 1 and parent_tracking_sample.streams[0]['streamName'] == '.*':
        # if there's only one streamName in MST, and it's global then we have to set the streamName ourselves
        stream_name = "customStreamName"
    else:
        # choose one streamName from MST (that's not the global .*) to include in the SST
        parent_tracking_sample.streams = [i for i in parent_tracking_sample.streams if i['streamName'] != '.*']
        stream_name = parent_tracking_sample.streams[0]['streamName']

    sst_with_parent_tracking_info = generator.create_token(parent_tracking_sample.id,
                                                           parent_tracking_sample.token,
                                                           stream_name,
                                                           parent_tracking_sample.tracking)
    return sst_with_parent_tracking_info


def with_custom_tracking(generator: TokenGenerator):
    custom_tracking_sample = get_sample('../../sample-json/sampleSSTWithNoParentTracking.json')

    # If the MST has streamNames, then the SST streamName has to match with atleast one in MST streamNames.
    # If there's only Regex in there (so global ".*"), then we need to specify an actual streamName to be used

    if len(custom_tracking_sample.streams) == 1 and custom_tracking_sample.streams[0]['streamName'] == '.*':
        # if there's only one streamName in MST, and it's global then we have to set the streamName ourselves
        stream_name = "customStreamName"
    else:
        # choose one streamName from MST (that's not the global .*) to include in the SST
        custom_tracking_sample.streams = [i for i in custom_tracking_sample.streams if i['streamName'] != '.*']
        stream_name = custom_tracking_sample.streams[0]['streamName']

    custom_tracking_id = Tracking("custom_tracking_id")
    sst_with_custom_tracking_info = generator.create_token(custom_tracking_sample.id,
                                                           custom_tracking_sample.token,
                                                           stream_name,
                                                           custom_tracking_id.__dict__)
    return sst_with_custom_tracking_info


def main():
    generator = TokenGenerator()

    # If there is no TrackingID on the Subscribe Token, we don't need to set one on the Self Signed Token
    sst_with_no_tracking_info = with_no_tracking_info(generator)

    # If there is a TrackingID in the Subscribe Token, we need to set the same TrackingID on the Self Signed Token
    sst_with_parent_tracking_info = with_parent_tracking(generator)

    # If there is no TrackingID in the Subscribe Token, we can set a Custom TrackingID for a specific Self Signed Token
    sst_with_custom_tracking_info = with_custom_tracking(generator)

    print(f"\n"
          f"    SST with no TrackingID: {sst_with_no_tracking_info}\n"
          f"    SST with parent TrackingID: {sst_with_parent_tracking_info}\n"
          f"    SST with custom TrackingID: {sst_with_custom_tracking_info}\n"
          f"    ")


if '__main__' == __name__:
    main()
