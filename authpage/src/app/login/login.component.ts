import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { API } from '../api.config';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css', '../sharedstyles/form.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  readonly headers = new HttpHeaders().set('Content-Type', 'application/json');

  status: number

  constructor(private client: HttpClient) {

  }

  ngOnInit(): void {
      this.loginForm = new FormGroup({
        login: new FormControl(null, Validators.required),
        password: new FormControl(null, Validators.required)
      })
  }

  OnSubmit() {
    this.client.post(`${API}/User/login`, 
        JSON.stringify(this.loginForm.getRawValue()), 
        {headers: this.headers}).subscribe({
          next: data => this.status = (data as any)["status"],
          error: error => this.status = error.error["status"]
        });
  }
}
