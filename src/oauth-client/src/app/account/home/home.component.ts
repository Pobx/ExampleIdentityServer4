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
    this.authorizeValue = this.authService.authorizationHeaderValue;
    this.name = this.authService.name;
  }

  clickSignOut() {
    this.authService.signout();
  }
}
