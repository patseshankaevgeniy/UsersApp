import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { AccessTokenService } from './access-token.service';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class AccessTokenInterceptor implements HttpInterceptor {
  
  constructor(
    private readonly accessTokenService: AccessTokenService,
    private readonly authenticationService: AuthService
  ) { }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (this.authenticationService.isAuthenticated) {
      req = req.clone({
        setHeaders: {
          Authorization: 'Bearer ' + this.accessTokenService.getToken(),
        },
      });
    }
    return next.handle(req);
  }
}