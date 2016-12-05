Based on https://github.com/angular/universal-starter (2016-12-04)

## Modification
* Rename file README.md to README2.md
* File app.module.ts line 11 add "import { AppComponent } from '../../../../Client/app/app.component';"
* File app.module.ts line 25 add "providers: [ { provide: 'paramsData', useValue: JSON.stringify({ Name: "Data from app.module.ts" }) } ]"
* File node.module.ts line 7 add "import { AppComponent } from '../../../Client/app/app.component';"
* File package.json line 68 add ""@ng-bootstrap/ng-bootstrap": "^1.0.0-alpha.9","
* File package.json line 69 add ""bootstrap": "^4.0.0-alpha.5""

## Run
* npm run build:prod:ngc
* npm run server