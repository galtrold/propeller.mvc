#!/bin/bash

echo $doMerge

# Copy propellerlogger bninary and configuration to website folder.
mkdir -p /var/www/bin/
mkdir -p /var/www/App_Config/Include/Propeller.Logger
cp /app/lib/propeller.logger.dll /var/www/bin/propeller.logger.dll
cp /app/config/propellerlog.config /var/www/App_Config/Include/Propeller.Logger/propellerlog.config

# merge config.web for Sitecore version lower than v8.1
if [ $doMerge = "true" ]
then
	echo "run merge script"
    npm install 
	nodejs /app/config_merge.js
	echo "...merge script completed."
fi


echo "Startup ELK stack"
sh /usr/local/bin/start.sh
