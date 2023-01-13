# Requirements
* .NET 6.0 SDK - https://dotnet.microsoft.com/en-us/download/dotnet/6.0

# Setup & Build
Run from inside csproj directory.
```
dotnet restore
dotnet build
```

# Running sample
Run from inside csproj directory.
```
dotnet run
```

# Additional Notes
`System.IdentityModel.Tokens.Jwt` will by default always add the `nbf` (NotBefore) field, which will be set to the
current timestamp when token was generated.
