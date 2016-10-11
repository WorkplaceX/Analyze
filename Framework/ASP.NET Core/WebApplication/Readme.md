# ASP.NET Core Angular2 Server Side Rendering

## Install
* npm run installAll (Installs "Angular" and "Angular Universal" and "ASP.NET Core" and runs type script compile for "Angular")
* Open http://localhost:5000/Angular/Debug.html one time. (This copies all necessary files to "wwwroot/Angular")

## Build
* npm run buildAngular (Builds "Angular" and "Angular Universal" and copies file AngularUniversalServer.js to ASP.NET Core)

# File and folder description
Folder "node_modules"
- Used for server side rendering.

File "AngularUniversalServer.js"
- Is the output of "Angular Universal/dist/server/index.js"

Folder "wwwroot/Angular"
- Contains only necessary files for publish
- Gets populated when "AngularDebug.cshtml" runs the first time.
