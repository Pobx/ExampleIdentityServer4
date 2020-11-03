import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { RequestLoginModel } from 'src/app/shared/models/RequestLoginModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  requestLoginModel: RequestLoginModel;
  myForm: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService) {}
  title: string;
  ngOnInit(): void {
    this.title = 'Login...';
  }

  login() {
    this.authService.login();
  }
}
