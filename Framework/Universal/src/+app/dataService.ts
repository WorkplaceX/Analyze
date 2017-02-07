import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import * as util from './util';
import {Observable} from 'rxjs/Rx';
import 'rxjs/add/operator/map';

declare var browserData: any; // Params from browser

export class Data {
    Name: string;
    IsDataGet: boolean; // GET not POST data when debugging client. See also file json.json
    VersionClient: string; // Angular client version.
    VersionServer: string; // Angular client version.
    List:any;
    IsBrowser:any;
    Session:string;
    RequestLog: string;
    ResponseCount: number;
}

@Injectable()
export class DataService {

    data: Data;

    log: string;

    RequestCount: number;

    http: Http;

    constructor( @Inject('angularData') angularData: string, http: Http) {
        this.http = http;
        // Default data
        this.data = new Data();
        this.log = "";
        this.RequestCount = 0;
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
        if (this.data.IsBrowser == true) {
            this.update();
        }
    }

    update() {
        this.RequestCount += 1;
        if (this.data.IsDataGet == true) {
            // GET for debug
            this.log += "Send GET; "
            this.http.get('json.json')
            .map(res => res)
            .subscribe(
                body => this.data = <Data>(body.json()),
                err => this.log += err + "; ",
                () => this.log += "Receive; "
            );
        } else {
            // POST
            this.log += "Send POST; ";
            this.http.post('json.json', JSON.stringify(this.data))
            .map(res => res)
            .subscribe(
                body => { 
                    var dataReceive: Data = <Data>(body.json());
                    this.data = dataReceive;
                },
                err => this.log += err + "; ",
                () => this.log += "Receive; "
            );
        }
    }
}