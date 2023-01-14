# Install Ubuntu 22.04
Installation notes for all samples on Ubuntu 22.04 machine.

## NodeJS
```bash
sudo mkdir -p /usr/local/share/keyrings
wget -nv -O- 'https://deb.nodesource.com/gpgkey/nodesource.gpg.key' | sudo tee /usr/local/share/keyrings/nodesource.asc >/dev/null

distroName="$(lsb_release -s -c)"

aptFile="deb [ signed-by=/usr/local/share/keyrings/nodesource.asc ] https://deb.nodesource.com/node_18.x ${distroName} main
deb-src [ signed-by=/usr/local/share/keyrings/nodesource.asc ] https://deb.nodesource.com/node_18.x ${distroName} main
"
sudo tee /etc/apt/sources.list.d/nodesource.list <<< "${aptFile}"

sudo apt-get update -y
sudo apt-get install -y nodejs build-essential
sudo npm install -g npm
```

## Dotnet
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

## Java
```bash
sudo apt-get update -y
sudo apt-get install -y openjdk-17-jdk-headless maven
```