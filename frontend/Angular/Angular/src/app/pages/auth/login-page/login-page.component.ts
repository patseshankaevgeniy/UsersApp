import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SignIn } from 'src/app/models/signIn.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  form: FormGroup = new FormGroup({
    email: new FormControl(null, [Validators.required, Validators.email]),
    password: new FormControl(null,[Validators.required, Validators.minLength(6)])
  })

  constructor( 
    private readonly authService: AuthService
  ) {
    
   }

  ngOnInit() {  
  }
  
  onSubmit(){
    this.authService.login(new SignIn(this.form.value.email, this.form.value.password)).subscribe(
      ()=> console.log('Login success'),
      error => {
        console.warn(error)
      }      
    )
  }
}
