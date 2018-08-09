import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { _throw } from 'rxjs/observable/throw';
import { catchError, tap, map } from 'rxjs/operators';
import { BaseService } from './base-service';

@Injectable()

export class AirportService extends BaseService {


  constructor(private http: HttpClient) {
    super();
  }

  getAirports(): Observable<IAirport[]> {
    var url = this.baseApiUrl + this.getAirportsUrl;
    return this.http.get<IAirport[]>(url);
  }

  getDestinationAirports(originAirport): Observable<IAirport[]> {
    var url = this.baseApiUrl + this.getDestinationAirportsUrl + originAirport;
    return this.http.get<IAirport[]>(url);
  }

  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  } 

}
