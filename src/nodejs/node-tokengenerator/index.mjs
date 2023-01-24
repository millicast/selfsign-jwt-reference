import { readFileSync } from 'node:fs';
import TokenGenerator from './TokenGenerator.mjs';
import Tracking from "./Tracking.mjs";

const generator = new TokenGenerator();

// If there is no TrackingID on the Subscribe Token, we dont need to set one on the Self Signed Token
let sstNoTrackingToken = createSSTWithNoTrackingInformation();

// If there is a TrackingID in the Subscribe Token, we need to set the same TrackingID on the Self Signed Token
let sstWithParentTrackingToken = createSSTWithParentTrackingInformation();

// If there is no TrackingID in the Subscribe Token, we can set a Custom TrackingID for a specific Self Signed Token
let sstWithCustomTracking = createSSTWithCustomTrackingInformation();

console.log('SST with no TrackingID: '+ sstNoTrackingToken +
    '\nSST with parent TrackingID: '+ sstWithParentTrackingToken +
    '\nSST with custom TrackingID: '+sstWithCustomTracking);


function createSSTWithNoTrackingInformation(){
  const sampleToken = JSON.parse(readFileSync('../../sample-json/sampleSST.json'));
  if (!sampleToken) {
    throw new Error('bad sample json');
  }

  return generator.createToken(sampleToken.id, sampleToken.token, sampleToken.streams[0].streamName, null, null, null);
}

function createSSTWithParentTrackingInformation(){
  const sampleToken = JSON.parse(readFileSync('../../sample-json/sampleSSTWithParentTracking.json'));
  if (!sampleToken) {
    throw new Error('bad sample json');
  }

  return generator.createToken(sampleToken.id, sampleToken.token, sampleToken.streams[0].streamName, null, null, sampleToken.tracking);
}

function createSSTWithCustomTrackingInformation(){
  const sampleToken = JSON.parse(readFileSync('../../sample-json/sampleSSTWithNoParentTracking.json'));
  if (!sampleToken) {
    throw new Error('bad sample json');
  }

  return generator.createToken(sampleToken.id, sampleToken.token, sampleToken.streams[0].streamName, null, null, new Tracking("customTrackingId"));
}