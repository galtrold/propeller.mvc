from xml.etree import ElementTree

with open('c:\projects\FIN.DK\www\Web.config', 'rt') as f:
    tree = ElementTree.parse(f)

for node in tree.findall('./configuration/log4net/appender'):
    print node.tag, node.attrib