
import json
from ptypes import SampleToken
from token_generator import TokenGenerator

def get_sample(filename:str='sample.json'):
    with open(filename) as f:
        data = f.read()
        return data

def main(sample_token:SampleToken):
    generator = TokenGenerator()
    self_signed_token = generator.create_token(sample_token.token_id, sample_token.token_string, sample_token.stream_name)
    print (self_signed_token)

if '__main__' == __name__:

    sample = json.loads(get_sample())
    if not sample:
        raise Exception('Bad sample json')
    sample_token = SampleToken(**sample)
    main(sample_token)
