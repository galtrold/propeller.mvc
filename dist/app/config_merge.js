const fs = require('fs');
var DOMParser = require('xmldom').DOMParser;
var XMLSerializer = require('xmldom').XMLSerializer;
var xpath = require('xpath')

//var configPath = 'c:/projects/FIN.DK/www/Web.config';
var configPath = '/var/www/Web.config';
//var mergeConfigPath = 'd:/Home/James/projects/Private/propeller.mvc/src/Framework/PropellerLogger/bin/sc81/App_Config/Include/propeller.logger/propellerlog.config';
var mergeConfigPath = '/var/www/App_Config/Include/Propeller.Logger/propellerlog.config';

var xmlData = fs.readFileSync(configPath, "ascii");
var mergeData = fs.readFileSync(mergeConfigPath, "utf8");

var webConfig = new DOMParser().parseFromString(xmlData);
var mergeConfig = new DOMParser().parseFromString(mergeData);

var propellerAppender = xpath.select("/configuration/sitecore/log4net/appender", mergeConfig)[0];
var rootAppenderReference = xpath.select("/configuration/sitecore/log4net/root/appender-ref", mergeConfig)[0];

var isPropellerAppenderMissing = xpath.select("/configuration/log4net/appender[@name='UdpTextAppender']", webConfig).length === 0;

console.log("appender missing :" + isPropellerAppenderMissing)
if(isPropellerAppenderMissing){
    var log4netNode = xpath.select("/configuration/log4net", webConfig)[0];
    log4netNode.appendChild(propellerAppender) 
}

var isUdpAppenderRefferenceMissing = xpath.select("/configuration/log4net/root/appender-ref[@ref='UdpTextAppender']", webConfig).length === 0;
console.log("appenderRef missing :" + isPropellerAppenderMissing)
if(isUdpAppenderRefferenceMissing){
    var appenderRoot = xpath.select("/configuration/log4net/root", webConfig)[0];
    appenderRoot.appendChild(rootAppenderReference);
}

if(isPropellerAppenderMissing || isUdpAppenderRefferenceMissing){
    var resultDoc = new XMLSerializer().serializeToString(webConfig);
    fs.writeFileSync(configPath, resultDoc );
}

console.log("Config merge complete!");
