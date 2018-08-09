import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { _throw } from 'rxjs/observable/throw';
import { catchError, tap, map } from 'rxjs/operators';
import { BaseService } from './base-service';

@Injectable()

export class StatsService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  getNumberOfRequestsWithStatusCode(statusCode: string): Observable<number> {
    var url = this.baseApiUrl + this.getNumberOfRequestsWithStatusCodeUrl + statusCode;
    return this.http.get<number>(url);
  }

  getNumberOfRequestsStartingWithStatusCode(statusCode: string): Observable<number> {
    var url = this.baseApiUrl + this.getNumberOfRequestsStartingWithStatusCodeUrl + statusCode;
    return this.http.get<number>(url);
  }

  getNumberOfRequestsByStatusCode(): Observable<any[]> {
    var url = this.baseApiUrl + this.getNumberOfRequestsByStatusCodeUrl;
    return this.http.get<any[]>(url);
  }

  getRequestStats(): Observable<any> {
    var url = this.baseApiUrl + this.getRequestStatsUrl;
    return this.http.get<any>(url);

  }

}
