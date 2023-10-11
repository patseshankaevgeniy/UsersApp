import { Injectable } from "@angular/core";
import { User } from "../models/user.model";

import { UserService } from "../services/user.service";
import { AccessTokenService } from "../services/access-token.service";

@Injectable({
    providedIn: 'root',
  })
  export class AppStore {

    private _currentUser?: User

    constructor(
        private readonly usersService: UserService,
        private readonly accessTokenService: AccessTokenService
    ){}

    init(): Promise<void> {
        
    
        return new Promise((resolve) => {
          // If user not loged in (login or sign up pages)
          if (!this.accessTokenService.isTokenValid()) {
            resolve();
            return;
          }

          this.usersService.getCurrentUserWithRoles().then((currentUser) => {
            this._currentUser = currentUser;
          })
        });
    }
  }