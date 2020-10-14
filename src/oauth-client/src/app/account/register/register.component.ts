import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { AuthService } from '../../core/authentication/auth.service';
import { UserRegistration } from '../../shared/models/user.registration';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  success: boolean;
  error: string;
  userRegistration: UserRegistration = { name: '', email: '', password: '' };
  submitted: boolean;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.submitted = false;
  }

  onSubmit() {
    this.authService.register(this.userRegistration).subscribe(
      (result) => {
        if (result) {
          this.success = true;
        }
      },
      (error) => {
        this.error = error;
      }
    );
  }
}
