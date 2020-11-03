import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { RequestRegisterModel } from 'src/app/shared/models/RequestRegisterModel';
import { AuthService } from '../../core/authentication/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  submitted: boolean;
  success: boolean;
  error: string;
  requestRegisterModel: RequestRegisterModel;
  myForm: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService) {}

  ngOnInit(): void {
    this.submitted = false;

    this.requestRegisterModel = {
      name: null,
      email: null,
      password: null,
    };

    this.myForm = this.fb.group({
      name: [null],
      email: [null],
      password: [null],
    });

    this.authService.getUserData();
  }

  onSubmit() {
    this.requestRegisterModel = Object.assign(
      this.requestRegisterModel,
      this.myForm.getRawValue()
    );

    this.authService.register(this.requestRegisterModel).subscribe(
      (result) => {
        console.log(result);
        if (result) {
          this.success = true;
        }
      },
      (error) => {
        console.log(error);
        this.error = error;
      }
    );
  }
}
