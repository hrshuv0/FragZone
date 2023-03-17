import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { AccountService } from "../../_services/account.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{

  loginForm! : FormGroup;

  constructor(private fb: FormBuilder, public authService: AccountService, private router: Router) {
  }

  ngOnInit(): void {

    this.createLoginForm();
  }

  login() {
    console.log(this.loginForm.value);
    this.authService.login(this.loginForm.value).subscribe(next =>{
      console.log('logged in successful');
    }, error => {
      console.log(error);
    }, () =>{
      this.router.navigate(['']);
    });
  }

  createLoginForm(){
    this.loginForm = this.fb.group({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(4)])
    });
  }
}
