require 'json'
require_relative 'token_generator'
require_relative 'tracking'

def create_SST_with_no_tracking_information(generator)
  sample_token = JSON.parse(File.read('../sample-json/sampleSST.json'))

  # If the MST has streamNames, then the SST streamName has to match with atleast one in MST streamNames.
  # If there's only Regex in there (so global ".*"), then we need to specify an actual streamName to be used
  if (sample_token['streams'].length == 1 && sample_token['streams'][0].first[1] == '.*')
    # if there's only one streamName in MST and it's global then we have to set the streamName ourselves
    stream_name = "customStreamName"
  else
    # choose one streamName from MST (that's not the global .*) to include in the SST
    sample_token['streams'].delete_if { |h| h['streamName'] == '.*'}
    stream_name = sample_token['streams'][0]
  end


  return generator.create_token(sample_token['id'], sample_token['token'], stream_name, nil, nil, nil)
end

def create_SST_with_custom_tracking_information(generator)
  sample_token = JSON.parse(File.read('../sample-json/sampleSSTWithNoParentTracking.json'))

  # If the MST has streamNames, then the SST streamName has to match with atleast one in MST streamNames.
  # If there's only Regex in there (so global ".*"), then we need to specify an actual streamName to be used
  if (sample_token['streams'].length == 1 && sample_token['streams'][0].first[1] == '.*')
    # if there's only one streamName in MST and it's global then we have to set the streamName ourselves
    stream_name = "customStreamName"
  else
    # choose one streamName from MST (that's not the global .*) to include in the SST
    sample_token['streams'].delete_if { |h| h['streamName'] == '.*'}
    stream_name = sample_token['streams'][0]
  end

  customTracking = Tracking.new("custom_tracking_id").to_hash
  return generator.create_token(sample_token['id'], sample_token['token'], stream_name, nil, nil, customTracking)
end

def create_SST_with_parent_tracking_information(generator)
  sample_token = JSON.parse(File.read('../sample-json/sampleSSTWithParentTracking.json'))

  # If the MST has streamNames, then the SST streamName has to match with atleast one in MST streamNames.
  # If there's only Regex in there (so global ".*"), then we need to specify an actual streamName to be used
  if (sample_token['streams'].length == 1 && sample_token['streams'][0].first[1] == '.*')
    # if there's only one streamName in MST and it's global then we have to set the streamName ourselves
    stream_name = "customStreamName"
  else
    # choose one streamName from MST (that's not the global .*) to include in the SST
    sample_token['streams'].delete_if { |h| h['streamName'] == '.*'}
    stream_name = sample_token['streams'][0]
  end

  return generator.create_token(sample_token['id'], sample_token['token'], stream_name, nil, nil, sample_token['tracking'])
end

generator = TokenGenerator.new

# If there is no TrackingID on the Subscribe Token, we dont need to set one on the Self Signed Token
sst_with_no_tracking_information = create_SST_with_no_tracking_information generator

# If there is a TrackingID in the Subscribe Token, we need to set the same TrackingID on the Self Signed Token
sst_with_parent_tracking_information = create_SST_with_parent_tracking_information generator

# If there is no TrackingID in the Subscribe Token, we can set a Custom TrackingID for a specific Self Signed Token
sst_with_custom_tracking_information = create_SST_with_custom_tracking_information generator

puts('SST with no TrackingID: '+ sst_with_no_tracking_information)
puts
puts('SST with parent TrackingID: '+ sst_with_parent_tracking_information)
puts
puts('SST with custom TrackingID: '+ sst_with_custom_tracking_information)