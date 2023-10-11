import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AppLayoutComponent } from "./pages/app-layout.component";

import { LoginPageComponent } from "./pages/auth/login-page/login-page.component";

import { SignUpPageComponent } from "./pages/auth/sign-up-page/sign-up-page.component";
import { UserComponent } from "./pages/users/user.component";
import { AuthLayoutComponent } from "./pages/layouts/auth-layout/auth-layout.component";
import { SiteLayoutComponent } from "./pages/layouts/site-layout/site-layout.component";
import { AuthGuard } from "./services/auth.guard";

export const basePath = '';
export const usersPath = 'users';
export const rolesPath = 'roles';
export const loginPath = 'login';
export const signupPath = 'registration'


const routes: Routes = [

  {
    path: basePath,
    component: AppLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: basePath,
        redirectTo: usersPath,
        pathMatch: 'full'
      },
    ]
  },
  {
    path: basePath,
    component: AuthLayoutComponent,
    children: [
      {
        path: loginPath,
        component: LoginPageComponent,
      },
      {
        path: signupPath,
        component: SignUpPageComponent,
      }
    ],
  },
  {
    path: basePath,
    component: AuthLayoutComponent,
  },
  {
    path: '**',
    redirectTo: basePath
  },

  // {
  //   path: '', component: AuthLayoutComponent, children: [
  //     {path: '', redirectTo: 'login', pathMatch: 'full'},
  //     {path: loginPath, component: LoginPageComponent},
  //     {path: signupPath, component: SignUpPageComponent}
  //   ]
  // },
  // {
  //   path: '', component: AppLayoutComponent, children: [
  //     {
  //       path: usersPath,
  //       component: UserComponent
  //     },
  //   ]
  // }
     
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
