import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import * as util from './util';

declare var browserData: any; // Params from browser

export class Data {
    Name: string;
}

@Injectable()
export class DataService {

    data: Data;

    constructor( @Inject('angularData') angularData: string) {
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
    }
}