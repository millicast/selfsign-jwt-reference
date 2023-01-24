# Java reference

## Prerequisites

* OpenJDK >= 17 - <https://jdk.java.net/17/>
* Maven - <https://maven.apache.org/download.cgi>

```bash
sudo apt-get update -y
sudo apt-get install -y openjdk-17-jdk-headless maven
```

## Setup & Build

Run from inside directory with pom.xml.

```bash
mvn clean compile
```

## Running the sample

Run from inside directory with pom.xml.

```bash
mvn --quiet exec:java -Dexec.mainClass=io.dolby.streaming.Main
```

## Additional Notes

Two sample implementations made with the two popular Java JWT signing libraries.

* Auth0TokenGenerator - <https://github.com/auth0/java-jwt>
* JavaJwtTokenGenerator - <https://github.com/jwtk/jjwt>

The other dependencies in sample are only used for helper work not easily achievable in base JDK.

* com.google.code.gson - JSON deserialization of sample.json
* com.fasterxml.jackson.core - Mapping strongly typed Class to Map. Used by both JWT libraries to add claims.
