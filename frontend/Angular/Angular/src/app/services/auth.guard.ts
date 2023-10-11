import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import { AuthService } from './auth.service';
import { loginPath } from '../app-routing.module';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {

  constructor(
    private readonly router: Router,
    private readonly authenticationService: AuthService,
  ) { }

  canActivate() {
    if (!this.authenticationService.isAuthenticated) {
      
      this.router.navigate([loginPath]);
      return false;
    }

    return true;
  }
}