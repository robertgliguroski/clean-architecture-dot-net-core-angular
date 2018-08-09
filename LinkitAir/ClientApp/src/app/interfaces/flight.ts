interface IFlight {
  flightInstanceId: number,
  flightCode: string,
  originAirportName: string,
  destinationAirportName: string,
  originCityName: string,
  destinationCityName: string,
  departureTime: DateTimeFormat,
  arrivalTime: DateTimeFormat,
  price: number
}
