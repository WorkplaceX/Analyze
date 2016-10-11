#Demo for Angular2 Universal Isomorphic
Angular2 Universal can pre- render html server side. See also: https://github.com/angular/universal

For simplicity in this demo it's all done client side. The generated html is then displayed as alert.

![alt tag](doc/ScreenShot.png)

#Prerequiste
nodejs.org (Download https://nodejs.org/en/download/)

npmjs.com (Included in nodejs.org)
#Install
Run the following commands from command prompt: 

**npm install**

Downloads and installs packages defined in file package.json to folders node_modules/ and typings/

**npm run tsc**

Runs type script compiler. Generates *.js and *.map files into folder app/

**npm run webpack**

Runs webpack and generates files to folder dist/

![alt tag](doc/ScreenShotWebPack.png)