import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/authentication/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  constructor(private authService: AuthService) {}
  authorizeValue: string;
  name: string;
  ngOnInit(): void {
    setTimeout(() => {
      this.authorizeValue = this.authService.authorizationHeaderValue;
      this.name = this.authService.name;
    }, 2000);
  }

  clickSignOut() {
    this.authService.signout();
  }
}
