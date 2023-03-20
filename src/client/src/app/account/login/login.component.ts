import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { AccountService } from "../../_services/account.service";
import { Router  } from "@angular/router";
import { AlertifyService } from "../../_services/alertify.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{

  loginForm! : FormGroup;

  constructor(private fb: FormBuilder,
              public authService: AccountService,
              private router: Router,
              private alertify: AlertifyService) {
  }

  ngOnInit(): void {

    this.createLoginForm();
  }

  login() {
    this.authService.login(this.loginForm.value).subscribe(next =>{
      this.alertify.success('logged in successful');
    }, error => {
      this.alertify.error(error);
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
