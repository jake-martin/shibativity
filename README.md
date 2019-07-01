# Shibativity
May 2019 Platform Project for DevEx. Note: the code in this project does not follow the Relativity Platform's best development practices as it was a learning exercise. Licensed under GPL 3.0.

The goal of this solution was to build a simple Relativity Application. The requirements were to utilize agents, event handlers, custom pages, and RDOs.

Shibativity Functionality:
 - Gets images of Shiba Inus (and sometimes birds) from shibe.online
 - Sends the image URLs to Cognitive Services in Azure
    - Descriptions and tags are generated by Computer Vision
 - Stores the Computer Vision metadata and image URL in an Image RDO
    - The Image RDO is created on a schedule by an agent
 - Allows the user to code whether Computer Vision was accurate or not using event handlers
 - Displays a custom page with information about the application
