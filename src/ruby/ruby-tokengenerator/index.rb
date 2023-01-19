require 'json'
require './token_generator'

sample_token = JSON.parse(File.read('./sample.json'))

generator = TokenGenerator.new
self_sign_token = generator.create_token(sample_token['tokenId'], sample_token['tokenString'], sample_token['streamName'])

puts(self_sign_token)
