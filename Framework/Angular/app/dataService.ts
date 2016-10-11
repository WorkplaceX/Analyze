import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

declare var params: any;

export class Data {
	Name: string;
}

@Injectable()
export class DataService {
  
  data: Data;

  constructor() {
      console.log("Data=" + JSON.stringify(this.data));
      var paramsLocal = null;
      if (typeof params !== 'undefined') {
          this.data = JSON.parse(params.data);
      } else {
          this.data = new Data();
          this.data.Name = "Data from dataService.ts";
	  }
  }
}