import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import * as util from './util';
import {Observable} from 'rxjs/Rx';
import 'rxjs/add/operator/map';

declare var browserData: any; // Params from browser

export class Data {
    Name: string;
    IsDataGet: boolean; // GET not POST when debugging client. See also file data.json
    VersionClient: string; // Angular client version.
    VersionServer: string; // Angular client version.
}

@Injectable()
export class DataService {

    data: Data;

    http: Http;

    constructor( @Inject('angularData') angularData: string, http: Http) {
        this.http = http;
        // Default data
        this.data = new Data();
        this.data.Name = "dataService.ts=" + util.currentTime();
        // Angular universal data
        if (angularData != null) {
            this.data = JSON.parse(angularData);
        }
        // Browser data
        if (typeof browserData !== 'undefined') {
            this.data = JSON.parse(browserData);
        }
        //
        this.data.VersionClient = util.versionClient();
        //
        if (this.data.IsDataGet == true) {
            this.update(); // For debug mode.
        }
    }

    update() {
        var result: Observable<Data>;
        if (this.data.IsDataGet == true) {
            // GET for debug
            console.log("Send GET");
            result = this.http.get('data.json').map(this.mapData);
        } else {
            // POST
            console.log("Send POST");
            console.log(JSON.stringify(this.data));
            result = this.http.post('data.json', JSON.stringify(this.data)).map(this.mapData);
        }
        result.forEach(x => this.data = x);
    }

    mapData(response: Response) : Data {
        console.log("Receive");
        let result = <Data>(response.json())
        result.VersionClient = util.versionClient();
        return result;
    }
}