import { readFileSync } from 'node:fs';
import TokenGenerator from './TokenGenerator.mjs';

const sampleToken = JSON.parse(readFileSync('sample.json'));
if (!sampleToken) {
  throw new Error('bad sample json');
}

const generator = new TokenGenerator();
const selfSignToken = generator.createToken(sampleToken['tokenId'], sampleToken['tokenString'], sampleToken['streamName']);

console.log(selfSignToken);
