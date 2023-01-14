# Requirements
* OpenJDK >= 17 - https://jdk.java.net/17/
* Maven - https://maven.apache.org/download.cgi

# Setup & Build
Run from inside directory with pom.xml.
```
mvn clean compile
```

# Running sample
Run from inside directory with pom.xml.
```
mvn --quiet exec:java -Dexec.mainClass=io.dolby.streaming.Main
```

# Additional Notes
Two sample implementations made with the two popular Java JWT signing libraries.
* Auth0TokenGenerator - https://github.com/auth0/java-jwt
* JavaJwtTokenGenerator - https://github.com/jwtk/jjwt

The other dependencies in sample are only used for helper work not easily achievable in base JDK.
* com.google.code.gson - JSON deserialization of sample.json
* com.fasterxml.jackson.core - Mapping strongly typed Class to Map. Used by both JWT libraries to add claims.
