import { readFileSync } from 'node:fs';
import TokenGenerator from './TokenGenerator.mjs';
import Tracking from "./Tracking.mjs";

// const sampleToken = JSON.parse(readFileSync('sampleSST.json'));
// if (!sampleToken) {
//   throw new Error('bad sample json');
// }

const generator = new TokenGenerator();

//const selfSignToken = generator.createToken(sampleToken.tokenId, sampleToken.tokenString, sampleToken.streamName);
let sstNoTrackingToken = createSSTWithNoTrackingInformation();
let sstWithParentTrackingToken = createSSTWithParentTrackingInformation();
let sstWithCustomTracking = createSSTWithCustomTrackingInformation();

console.log('SST with no TrackingID: '+ sstNoTrackingToken +
    '\nSST with parent TrackingID: '+ sstWithParentTrackingToken +
    '\nSST with custom TrackingID: '+sstWithCustomTracking);
console.log();

function createSSTWithNoTrackingInformation(){
  const sampleToken = JSON.parse(readFileSync('sampleSST.json'));
  if (!sampleToken) {
    throw new Error('bad sample json');
  }

  //const selfSignToken = generator.createToken(sampleToken.tokenId, sampleToken.token, sampleToken.streamName);
  return generator.createToken(sampleToken.tokenId, sampleToken.token, sampleToken.streams[0].streamName, null, null, null);
}

function createSSTWithParentTrackingInformation(){
  const sampleToken = JSON.parse(readFileSync('sampleSSTWithParentTracking.json'));
  if (!sampleToken) {
    throw new Error('bad sample json');
  }

  return generator.createToken(sampleToken.tokenId, sampleToken.token, sampleToken.streams[0].streamName, null, null, sampleToken.tracking);
}

function createSSTWithCustomTrackingInformation(){
  const sampleToken = JSON.parse(readFileSync('sampleSSTWithNoParentTracking.json'));
  if (!sampleToken) {
    throw new Error('bad sample json');
  }

  return generator.createToken(sampleToken.tokenId, sampleToken.token, sampleToken.streams[0].streamName, null, null, new Tracking("customTrackingId"));
}