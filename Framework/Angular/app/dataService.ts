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
      var paramsLocal = null;
      if (typeof params !== 'undefined') {
          this.data = params.Data;
      } else {
          this.data = new Data();
          this.data.Name = "Data from dataService.ts";
	  }
  }
}