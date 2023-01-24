# C# .NET reference

## Prerequisites

* .NET 6.0 SDK - <https://dotnet.microsoft.com/en-us/download/dotnet/6.0>

```bash
distroVersion="$(lsb_release -s -r)"

aptPin='Package: *
Pin: origin "packages.microsoft.com"
Pin-Priority: 1001
'
sudo tee /etc/apt/preferences.d/dotnet-pmc.pref <<< "${aptPin}"

tmpFile="$(mktemp)"
wget -nv -O "${tmpFile}" "https://packages.microsoft.com/config/ubuntu/${distroVersion}/packages-microsoft-prod.deb"
sudo dpkg -i "${tmpFile}"
sudo apt-get update -y
sudo apt-get install -y dotnet-sdk-6.0
```

## Setup & Build

Run from inside csproj directory.

```bash
dotnet restore
dotnet build
```

## Running the sample

Run from inside csproj directory.

```bash
dotnet run
```

## Additional Notes

`System.IdentityModel.Tokens.Jwt` will by default always add the `nbf` (NotBefore) field, which will be set to the
current timestamp when token was generated.
