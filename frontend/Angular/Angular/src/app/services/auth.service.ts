import { Injectable } from "@angular/core";

import { User } from "../models/user.model";
import { Observable, map } from "rxjs";
import { ISignInDto, ISignUpDto, SignInDto, SignInResultDto, SignUpDto, UsersAPIClient } from "../clients/users-api.client";
import { SignIn } from "../models/signIn.model";
import { CreatedUser } from "../models/createdUser";
import { AccessTokenService } from "./access-token.service";
import { AppStore } from "../store/app.store";
import { Router } from "@angular/router";
import { basePath } from "../app-routing.module";

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    constructor(
        private readonly usersApiClient: UsersAPIClient,
        private readonly accessTokenService: AccessTokenService,
        private readonly appStore: AppStore,
        private readonly router: Router
    ){}

    get isAuthenticated(): boolean {
        return this.accessTokenService.isTokenValid();
    }

    signUp(user: CreatedUser): Observable<SignInResultDto>{
        return this.usersApiClient
            .signUp(new SignUpDto(this.mapToSignUpDto(user)))
            .pipe(map(({result}) => result));
    }

    login(user: SignIn): Observable<SignInResultDto> {
       return this.usersApiClient
        .signIn(new SignInDto(this.mapToSignInDto(user)))
        .pipe(map(({result}) => {
            if(result.succeeded){
                this.accessTokenService.setToken(result.token!);
                this.appStore.init()
                this.router.navigate([basePath])
            }
            return result;
        }));
    }

    private mapToSignInDto(user: SignIn): ISignInDto{
        return  {
            email: user.email,
            password: user.password
        }         
    }

    private mapToSignUpDto(user: CreatedUser): ISignUpDto{
        return {
            name: user.Name,
            age: user.Age,
            email: user.Email,
            password: user.Password
        }
    }
}