import { Component, OnInit } from '@angular/core';
import { IPublisher } from "../../_models/publisher";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { PublisherService } from "../../_services/publisher.service";
import { AlertifyService } from "../../_services/alertify.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-publisher-create',
  templateUrl: './publisher-create.component.html',
  styleUrls: ['./publisher-create.component.scss']
})
export class PublisherCreateComponent implements OnInit{

  publisher!: IPublisher;
  publisherForm!: FormGroup;

  constructor(private fb: FormBuilder,
              private publisherService: PublisherService,
              private alertify: AlertifyService,
              private router: Router) {
  }

  ngOnInit(): void {
    this.createPublisherForm();
  }

  createPublisherForm(){
    this.publisherForm = this.fb.group({
      name: new FormControl('', Validators.required)
    });

  }

  create() {
    this.publisher = Object.assign({}, this.publisherForm.value);
    this.publisherService.createPublisher(this.publisher).subscribe(() =>{
      this.alertify.success("Publisher Created Successfully");
      this.router.navigate(['/publisher']);
    }, error => {
      this.alertify.error(error);
    });
  }



}
