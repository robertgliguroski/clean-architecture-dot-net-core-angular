"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var auth_service_1 = require("./auth-service");
var AuthInterceptor = /** @class */ (function () {
    function AuthInterceptor(injector) {
        this.injector = injector;
    }
    AuthInterceptor.prototype.intercept = function (request, next) {
        var auth = this.injector.get(auth_service_1.AuthService);
        var token = (auth.isLoggedIn()) ? auth.getAuth().token : null;
        if (token) {
            request = request.clone({
                setHeaders: {
                    Authorization: "Bearer " + token
                }
            });
        }
        return next.handle(request);
    };
    return AuthInterceptor;
}());
exports.AuthInterceptor = AuthInterceptor;
//# sourceMappingURL=auth-interceptor-service.js.map