#!/bin/bash
# Copy propellerlogger binary and configuration to website folder.
mkdir -p /var/www/App_Config/Include/Propeller.Logger
cp /app/lib/propeller.logger.dll /var/www/bin/propeller.logger.dll
cp /app/config/propellerlog.config /var/www/App_Config/Include/Propeller.Logger/propellerlog.config

# merge config.web for Sitecore version lower than v8.1
if [ $doMerge = "true" ]
then
	echo "Propeller logger configured against Sitecore 8.0 or lower."
	echo "Merging UDP appender into Web.config"
	yarn global add xpath
	yarn global add xmldom
	nodejs /app/config_merge.js
fi

echo "Startup ELK stack"
exec /usr/local/bin/start.sh
