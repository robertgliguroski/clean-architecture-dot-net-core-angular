import { environment } from '../../environments/environment';
export class BaseService {

  baseApiUrl = environment.baseApiUrl;
  getAirportsUrl = environment.getAirportsUrl;
  getDestinationAirportsUrl = environment.getDestinationAirportsUrl;
  loginUrl = environment.loginUrl;
  getFlightForOriginDestinationDateUrl = environment.getFlightForOriginDestinationDateUrl;
  getFlightsOriginDestinationUrl = environment.getFlightsOriginDestinationUrl;
  getAlternativeFlightsOriginDestinationUrl = environment.getAlternativeFlightsOriginDestinationUrl;
  getNumberOfRequestsWithStatusCodeUrl = environment.getNumberOfRequestsWithStatusCodeUrl;
  getNumberOfRequestsStartingWithStatusCodeUrl = environment.getNumberOfRequestsStartingWithStatusCodeUrl;
  getRequestStatsUrl = environment.getRequestStatsUrl;
  getNumberOfRequestsByStatusCodeUrl = environment.getNumberOfRequestsByStatusCodeUrl;

  constructor() { }

}
