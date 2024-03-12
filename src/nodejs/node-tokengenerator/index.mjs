import { readFileSync } from 'node:fs';
import TokenGenerator from './TokenGenerator.mjs';
import Tracking from "./Tracking.mjs";

const generator = new TokenGenerator();
const regexStreamName = 'customStreamName';

// If there is no TrackingID on the Subscribe Token, we dont need to set one on the Self Signed Token
const sstNoTrackingToken = createSSTWithNoTrackingInformation();

// If there is a TrackingID in the Subscribe Token, we need to set the same TrackingID on the Self Signed Token
const sstWithParentTrackingToken = createSSTWithParentTrackingInformation();

// If there is no TrackingID in the Subscribe Token, we can set a Custom TrackingID for a specific Self Signed Token
const sstWithCustomTracking = createSSTWithCustomTrackingInformation();

// use basic SST with customViewerData
const sstWithCustomViewerData = createSSTWithCustomViewerData();

console.log('SST with no TrackingID: '+ sstNoTrackingToken +
    '\nSST with parent TrackingID: '+ sstWithParentTrackingToken +
    '\nSST with custom TrackingID: '+ sstWithCustomTracking +
    '\nSST with customViewerData: ' + sstWithCustomViewerData);


function createSSTWithNoTrackingInformation(){
  const sampleToken = getSampleToken();
  const streamName = getStreamName(sampleToken, regexStreamName);

  return generator.createToken(sampleToken.id, sampleToken.token, streamName);
}

function createSSTWithParentTrackingInformation(){
  const sampleToken = getSampleToken();
  const streamName = getStreamName(sampleToken, regexStreamName);

  return generator.createToken(sampleToken.id, sampleToken.token, streamName, null, null, sampleToken.tracking);
}

function createSSTWithCustomTrackingInformation(){
  const sampleToken = getSampleToken();
  const streamName = getStreamName(sampleToken, regexStreamName);

  return generator.createToken(sampleToken.id, sampleToken.token, streamName, null, null, new Tracking("customTrackingId"));
}

function createSSTWithCustomViewerData() {
  const sampleToken = getSampleToken();
  const streamName = getStreamName(sampleToken, regexStreamName);

  return generator.createToken(sampleToken.id, sampleToken.token, streamName, null, null, null, null,
      'uniqueViewer1234');
}

function getSampleToken(sampleTokenPath = '../../sample-json/sampleSST.json') {
  const token = JSON.parse(readFileSync(sampleTokenPath));
  if (!token) {
    throw new Error('bad sample json');
  }
  return token;
}

/**
 *
 * If the MST has streamNames, then the SST streamName has to match with at least one in MST streamNames.
 * If there's only Regex in there (so global ".*"), then we need to specify an actual streamName to be used
 * @param token
 * @param requestedStreamName
 * @returns {*}
 */
function getStreamName(token, requestedStreamName) {
  // default to first streamName in token which is not regex
  let streamName = token.streams.find(s => !s.isRegex)?.streamName;
  if (streamName) {
    return streamName;
  }

  // else choose specified streamName for regex tokens
  return requestedStreamName;
}
