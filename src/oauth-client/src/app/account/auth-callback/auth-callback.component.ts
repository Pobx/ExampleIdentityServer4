import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/core/authentication/auth.service';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.scss'],
})
export class AuthCallbackComponent implements OnInit {
  error: boolean;

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.error = false;
    const messageUrl = this.route.snapshot.fragment;
    if (messageUrl.indexOf('error') >= 0) {
      this.error = true;
    }

    this.authService.completeAuthentication();
    setTimeout(() => {
      this.router.navigate(['/home']);
    }, 2000);
  }
}
