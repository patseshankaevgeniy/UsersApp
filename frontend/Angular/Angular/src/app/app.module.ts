import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule, NgStyle } from '@angular/common';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { UserComponent } from './pages/users/user.component';
import { NavigationBarModule } from './navigation-bar/navigation-bar.module';
import { AuthLayoutComponent } from './pages/layouts/auth-layout/auth-layout.component';
import { SiteLayoutComponent } from './pages/layouts/site-layout/site-layout.component';
import { LoginPageComponent } from './pages/auth/login-page/login-page.component';
import { SignUpPageComponent } from './pages/auth/sign-up-page/sign-up-page.component';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppLayoutComponent } from './pages/app-layout.component';
import { USERS_API_BASE_URL, UsersAPIClient } from './clients/users-api.client';
import { environment } from 'src/environments/environment';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap'; 
import {MatSortModule} from '@angular/material/sort';
import { AccessTokenInterceptor } from './services/access-token.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    AppLayoutComponent,
    UserComponent,
    LoginPageComponent,
    SignUpPageComponent,
    AuthLayoutComponent,
    SiteLayoutComponent
  ],
  imports: [
    BrowserModule,
    NavigationBarModule,
    HttpClientModule,
    MatSortModule,
    CommonModule,
    NgStyle,
    NgbModule,   
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
          tokenGetter: () => localStorage.getItem('access_token'),
      },
  }),
  ],
  providers: [
    JwtHelperService,
    UsersAPIClient,
    {
      provide: USERS_API_BASE_URL,
      useValue: environment.clientsApiClientBaseUrl,
    },
    {
      provide: HTTP_INTERCEPTORS,
      multi: true,
      useClass: AccessTokenInterceptor,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
