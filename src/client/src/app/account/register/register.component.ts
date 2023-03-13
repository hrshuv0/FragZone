import { Component, OnInit } from '@angular/core';
import {AbstractControl, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit{

  registerForm! : FormGroup;

  constructor(private fb: FormBuilder) {
  }

  ngOnInit(): void {

    this.createRegisterForm();
  }

  createRegisterForm(){
    this.registerForm = this.fb.group({
      // username: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.email, Validators.required]),
      password: new FormControl('', [Validators.required, Validators.minLength(4)]),
      confirmPassword: new FormControl('', Validators.required)
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(g: AbstractControl){
    return g.get('password')?.value === g.get('confirmPassword')?.value ? null : {'mismatch':true};
  }

  register(){
    console.log(this.registerForm.value);
  }

}
