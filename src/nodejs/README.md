# Node.js reference

## Prerequisites

* Node.js >- 12.x <https://nodejs.org/en/download/>
* build-essential
* NPM

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

## Setup & Build

```bash
npm ci
```

## Running the sample

```bash
node index.mjs
```
