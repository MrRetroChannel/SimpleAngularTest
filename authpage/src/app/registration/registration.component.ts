import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { API } from '../api.config';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css', '../sharedstyles/form.css']
})
export class RegistrationComponent implements OnInit {
  registerForm: FormGroup;

  readonly headers = new HttpHeaders().set('Content-Type', 'application/json');

  submitStatus: number[] = []

  constructor(private client: HttpClient) {

  }

  ngOnInit(): void {
      this.registerForm = new FormGroup({
        email: new FormControl(null, Validators.required),
        username: new FormControl(null, Validators.required),
        password: new FormControl(null, Validators.required)
      });
  }

  OnSubmit() {
    this.client.post(`${API}/User/register`, 
        JSON.stringify(this.registerForm.getRawValue()), 
        {headers: this.headers}).subscribe({
          next: data => this.submitStatus = [Number(data)],
          error: error => console.log(this.submitStatus = error.error)
        });
  }
}
