require 'json'
require_relative 'token_generator'
require_relative 'tracking'

def create_SST_with_no_tracking_information(generator)
  sample_token = JSON.parse(File.read('../sample-json/sampleSST.json'))
  return generator.create_token(sample_token['id'], sample_token['token'], sample_token['streams'][0], nil, nil, nil)
end

def create_SST_with_custom_tracking_information(generator)
  sample_token = JSON.parse(File.read('../sample-json/sampleSSTWithNoParentTracking.json'))
  customTracking = Tracking.new("custom_tracking_id").to_hash
  return generator.create_token(sample_token['id'], sample_token['token'], sample_token['streams'][0], nil, nil, customTracking)
end

def create_SST_with_parent_tracking_information(generator)
  sample_token = JSON.parse(File.read('../sample-json/sampleSSTWithParentTracking.json'))
  return generator.create_token(sample_token['id'], sample_token['token'], sample_token['streams'][0], nil, nil, sample_token['tracking'])
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