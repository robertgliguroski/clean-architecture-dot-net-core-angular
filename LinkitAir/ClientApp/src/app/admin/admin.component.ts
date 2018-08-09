import { Component, OnInit } from '@angular/core';
import { StatsService } from '../services/stats-service';
import { AuthService } from '../services/auth-service';
import { Router } from "@angular/router";

import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  noOfRequestsByStatus: any;
  noOfRequestsStartingWith: any;
  noOfRequestsByResponseCode: any[];
  requestStats: any;

  constructor(private statsService: StatsService, public auth: AuthService, private router: Router) {
    this.noOfRequestsByStatus = {
      '200': 0
    };
    this.noOfRequestsStartingWith = {
      '4': 0,
      '5': 0
    }
  }

  ngOnInit() {
    if (!this.auth.isLoggedIn()) {
      this.router.navigate(["/login"]);
    } else {
      this.getNoOfRequestsWithStatus('200');
      this.getNoOfRequestsStartingWithStatus('4');
      this.getNoOfRequestsStartingWithStatus('5');
      this.getNumberOfRequestsByStatusCode();
      this.getRequestStats();
    }
  }

  getNoOfRequestsWithStatus(statusCode: string): void {
    this.statsService.getNumberOfRequestsWithStatusCode(statusCode).subscribe(
      data => { this.noOfRequestsByStatus[statusCode] = data; },
      err => console.log(err)
    );
  }

  getNoOfRequestsStartingWithStatus(statusCode: string): void {
    this.statsService.getNumberOfRequestsStartingWithStatusCode(statusCode).subscribe(
      data => { this.noOfRequestsStartingWith[statusCode] = data; },
      err => console.log(err)
    );
  }

  getNumberOfRequestsByStatusCode(): void {
    this.statsService.getNumberOfRequestsByStatusCode().subscribe(
      data => { this.noOfRequestsByResponseCode = data; console.log('data = '); console.dir(data); },
      err => console.log(err)
    );
  }

  getRequestStats(): void {
    this.statsService.getRequestStats().subscribe(
      data => { this.requestStats = data; },
      err => console.log(err)
    );
  }

}
