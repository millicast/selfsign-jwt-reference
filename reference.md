# Implementing SelfSign Sample Code for New Language
* Create language specific subdirectory in `src/`.
* Create `.gitignore` file as necessary for language specific files/dirs that should be ignored from version control.
* Create `README.md` which should outline the following:
  * What are the required language's sdk and/or tool versions.
  * Specific setup/build commands for sample code. This should include installing 3rd party packages.
  * Command to run sample
* Create a symlink to `src/sample.json` if necessary for language specific directory.

# Sample Interface
The following is pseudocode for an interface for samples. This pseudocode is not meant to impose any specific Case Style
or formatting. That should follow what is more appropriate for language. Else simply follow PascalCasing as done here.
```pseudo-code
interface TokenGenerator {
  constructor()
  constructor(string hmacAlg)

  string CreateToken(uint tokenId, string tokenString, string streamName,
      string[] allowedOrigins, string[] allowedIpAddresses,
      int expiresIn)
}
```

Two constructors are expected for the class, the parameterless constructor should default to the hmac algorithm `HS256`.

The `CreateToken()` function should return the base64 encoded JWT string.
* The first 3 parameters are all required: `tokenId`,`tokenString`, `streamName`.
* The 2 string array parameters are entirely optional: `allowedOrigins`, `allowedIpAddresses`
  * If they are ignored they should default to the language equivalent of an empty array/list.
* The final parameter expiresIn is also optional and should default to 60.

If the language does not fully support optional parameters in function declarations, switching to implementing the same
features with function overloading is acceptable.

# JwtPayload
The payload inside the JWT should always follow this layout: casing **does matter here**. All options should be
contained inside a field named `streaming`. `tokenType` currently always defaults to `Subscribe`. As mentioned
previously `allowedOrigins` and `allowedIpAddresses` should default to an empty array if not specified.
```psuedojson
{
  "streaming": {
    "tokenId": number,
    "tokenType": "Subscribe",
    "streamName": string,
    "allowedOrigins": string[],
    "allowedIpAddresses": string[]
  }
}
```

## Expected Format of Decoded JWT
The final JWT should resemble the following after being decoded with a tool like [jwt.io](http://jwt.io).

### Header
The default algorithm in most libraries is likely to be HS256.
We support the following: HS256, HS384, HS512.
```json
{
  "alg": "HS256",
  "typ": "JWT"
}
```

### Payload
The JWT signing library may or may not add a `nbf` (NotBefore) field.
This is supported, but not strictly required in our JWTs.
```json
{
  "streaming": {
    "tokenId": 1,
    "tokenType": "Subscribe",
    "streamName": "teststream",
    "allowedOrigins": [],
    "allowedIpAddresses": []
  },
  "exp": 1673600917,
  "iat": 1673600857
}
```