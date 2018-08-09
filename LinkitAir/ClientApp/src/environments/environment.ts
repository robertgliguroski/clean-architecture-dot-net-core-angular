// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  baseApiUrl: 'http://localhost:51736/api/',
  getAirportsUrl: 'airport/getairports/',
  getDestinationAirportsUrl: 'airport/getdestinationairports/',
  loginUrl: 'token/jwt/',
  getFlightForOriginDestinationDateUrl: 'flight/GetFlightWithDate/origin/',
  getFlightsOriginDestinationUrl: 'flight/GetFlights/origin/',
  getAlternativeFlightsOriginDestinationUrl: 'flight/GetAlternativeFlights/origin/',
  getNumberOfRequestsWithStatusCodeUrl: 'admin/CountRequestsWithCode/',
  getNumberOfRequestsStartingWithStatusCodeUrl: 'admin/CountRequestsStartingWith/',
  getRequestStatsUrl: 'admin/GetRequestStats/',
  getNumberOfRequestsByStatusCodeUrl: 'admin/CountRequestsByResponseCode/'
};
