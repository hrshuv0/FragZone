import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { AccountService } from "../../_services/account.service";
import { IUser } from "../../_models/user";
import { AlertifyService } from "../../_services/alertify.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit{

  user!: IUser;
  registerForm! : FormGroup;

  constructor(private fb: FormBuilder,
              private authService: AccountService,
              private alertify: AlertifyService,
              private router: Router) {
  }

  ngOnInit(): void {

    this.createRegisterForm();
  }

  createRegisterForm(){
    this.registerForm = this.fb.group({
      username: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.email, Validators.required]),
      password: new FormControl('', [Validators.required, Validators.minLength(4)]),
      confirmPassword: new FormControl('', Validators.required)
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(g: AbstractControl){
    return g.get('password')?.value === g.get('confirmPassword')?.value ? null : {'mismatch':true};
  }

  register(){
    if(this.registerForm.valid){
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe(() =>{
        this.alertify.success("Registration complete");
      }, error => {
        this.alertify.error(error.error);
      }, () =>{
        this.authService.login(this.user).subscribe(() =>{
          this.router.navigate(['']);
        });
      });
    }
  }

}
