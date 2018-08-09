import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { _throw } from 'rxjs/observable/throw';
import { catchError, tap, map } from 'rxjs/operators';
import { BaseService } from './base-service';

@Injectable()

export class FlightService extends BaseService{

  constructor(private http: HttpClient) {
    super();
  }

  getFlightForOriginAndDestinationAndDate(originAirport, destinationAirport, flightDate): Observable<IFlight> {
    var url = this.baseApiUrl + this.getFlightForOriginDestinationDateUrl + originAirport
      + "/destination/" + destinationAirport + "/flightdate/" + flightDate;
    return this.http.get<IFlight>(url);
  }

  getFlightsForOriginAndDestination(originAirport, destinationAirport, returnTicket): Observable<IFlight[]> {
    var url = this.baseApiUrl + this.getFlightsOriginDestinationUrl + originAirport
      + "/destination/" + destinationAirport + "/return/" + returnTicket;
    return this.http.get<IFlight[]>(url);
  }

  getAlternativeFlightsForOriginAndDestination(originAirport, destinationAirport, originalResultFlightInstanceId, returnTicket): Observable<any> {
    var altUrl = this.baseApiUrl + this.getAlternativeFlightsOriginDestinationUrl + originAirport
      + "/destination/" + destinationAirport + "/without/" + originalResultFlightInstanceId + "/return/" + returnTicket;

    return this.http.get<IFlight[]>(altUrl);
  }


  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  } 

}
