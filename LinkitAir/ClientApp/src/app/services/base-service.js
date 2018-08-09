"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../environments/environment");
var BaseService = /** @class */ (function () {
    function BaseService() {
        this.baseApiUrl = environment_1.environment.baseApiUrl;
        this.getAirportsUrl = environment_1.environment.getAirportsUrl;
        this.getDestinationAirportsUrl = environment_1.environment.getDestinationAirportsUrl;
        this.loginUrl = environment_1.environment.loginUrl;
        this.getFlightForOriginDestinationDateUrl = environment_1.environment.getFlightForOriginDestinationDateUrl;
        this.getFlightsOriginDestinationUrl = environment_1.environment.getFlightsOriginDestinationUrl;
        this.getAlternativeFlightsOriginDestinationUrl = environment_1.environment.getAlternativeFlightsOriginDestinationUrl;
        this.getNumberOfRequestsWithStatusCodeUrl = environment_1.environment.getNumberOfRequestsWithStatusCodeUrl;
        this.getNumberOfRequestsStartingWithStatusCodeUrl = environment_1.environment.getNumberOfRequestsStartingWithStatusCodeUrl;
        this.getRequestStatsUrl = environment_1.environment.getRequestStatsUrl;
        this.getNumberOfRequestsByStatusCodeUrl = environment_1.environment.getNumberOfRequestsByStatusCodeUrl;
    }
    return BaseService;
}());
exports.BaseService = BaseService;
//# sourceMappingURL=base-service.js.map