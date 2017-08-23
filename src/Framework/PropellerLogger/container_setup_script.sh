#!/bin/bash
mkdir /var/www 
mkdir -p /app/lib
mkdir -p /app/config
mkdir -p /var/www/bin/
mkdir -p /var/www/App_Config/Include/Propeller.Logger
apt-get update
apt-get install -y curl
curl -sL https://deb.nodesource.com/setup_6.x | bash
apt-get install nodejs
curl -sS https://dl.yarnpkg.com/debian/pubkey.gpg | apt-key add -
echo "deb https://dl.yarnpkg.com/debian/ stable main" | tee /etc/apt/sources.list.d/yarn.list
apt-get update
apt-get install -y yarn
yarn global add npm@5
