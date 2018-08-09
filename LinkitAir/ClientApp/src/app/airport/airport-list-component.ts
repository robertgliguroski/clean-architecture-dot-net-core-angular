import { Component, OnInit } from '@angular/core';
import { AirportService } from '../services/airport-service';
import { FlightService } from '../services/flight-service';
import { FormControl } from '@angular/forms';

import { Observable } from 'rxjs/Observable';
import { startWith } from 'rxjs/operators/startWith';
import { map } from 'rxjs/operators/map';

@Component({
  selector: 'airport-list',
  templateUrl: './airport-list.component.html',
  styleUrls: ['./airport-list-component.css']
})

export class AirportListComponent implements OnInit {
  originAirports: IAirport[];
  destinationAirports: IAirport[];
  console = console;
  selectedOrigin: IAirport;
  selectedDestination: IAirport;
  flightDate: string;
  flightWithDate: IFlight;
  flightsOriginDestination: IFlight[];
  alternativeFlights: IFlight[];
  originAirportsCtrl: FormControl;
  destinationAirportsCtrl: FormControl;
  filteredOriginAirports: Observable<any[]>;
  filteredDestinationAirports: Observable<any[]>;
  returnChecked = false;
  wasCheckedReturn = false;

  constructor(private airportService: AirportService, private flightService: FlightService) {
    this.originAirportsCtrl = new FormControl();
    this.destinationAirportsCtrl = new FormControl();
  }

  getOriginAirports(): void {
    this.airportService.getAirports().subscribe(
      data => { this.originAirports = data },
      err => console.log(err)
    );
  }

  filterAirports(val: any, airports: any) {
    return airports.filter(airport => {
      let name = val.name ? val.name : val;
      return airport.name.toLowerCase().indexOf(name.toLowerCase()) >= 0 ||
        airport.code.toLowerCase().indexOf(name.toLowerCase()) >= 0 ||
        airport.description.toLowerCase().indexOf(name.toLowerCase()) >= 0;
    });
  }

  ngOnInit() {
    this.airportService.getAirports().subscribe(
      data => {
        this.originAirports = data;
        this.filteredOriginAirports = this.originAirportsCtrl.valueChanges
          .pipe(
          startWith(''),
          map(val => this.filterAirports(val, this.originAirports))
         // map(name => name ? this.filterAirports(name) : this.originAirports.slice())
          );
      },
      err => console.log(err)
    );    
  }

  onOriginSelected(originAirport) {
    this.selectedOrigin = originAirport;
    this.selectedDestination = null;
    this.airportService.getDestinationAirports(originAirport.id).subscribe(
      data => {
        this.destinationAirports = data;
        this.filteredDestinationAirports = this.destinationAirportsCtrl.valueChanges
          .pipe(
          startWith(''), 
          map(val => this.filterAirports(val, this.destinationAirports))

          );
      },
      err => console.log(err)
    );
  }

  onDestinationSelected(destinationAirport) {
    this.selectedDestination = destinationAirport;
  }

  displayFn(airport: any): string {
    return airport ? airport.name : airport;
  }


  getFlightWithDate() {
    this.flightService.getFlightForOriginAndDestinationAndDate(this.selectedOrigin, this.selectedDestination, this.flightDate).subscribe(
      data => { this.flightWithDate = data },
      err => console.log(err)
    );
  }

  searchFlightsOriginDestination() {

    return this.flightService.getFlightsForOriginAndDestination(this.selectedOrigin.id,
      this.selectedDestination.id, this.returnChecked).subscribe(
      data => {
        this.flightsOriginDestination = data;
        this.alternativeFlights = [];
        this.wasCheckedReturn = this.returnChecked;
      },
      err => console.log(err)
    );
  }

  searchAlternativeFlights(originalResultFlightInstanceId: number) {

    return this.flightService.getAlternativeFlightsForOriginAndDestination(this.selectedOrigin.id,
      this.selectedDestination.id, originalResultFlightInstanceId, this.returnChecked).subscribe(
      data => { this.alternativeFlights = data; },
      err => console.log(err)
      );
  }

}
