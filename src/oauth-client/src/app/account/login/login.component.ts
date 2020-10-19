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

  ngOnInit(): void {
    this.requestLoginModel = {
      email: null,
      password: null,
    };

    this.myForm = this.fb.group({
      email: [null],
      password: [null],
    });
  }

  onSubmit() {}
}
