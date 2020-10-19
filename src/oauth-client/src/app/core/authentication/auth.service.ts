import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { UserManager, UserManagerSettings, User } from 'oidc-client';
import { BehaviorSubject } from 'rxjs';

import { BaseService } from '../../shared/base.service';
import { ConfigService } from '../../shared/config.service';
import { RequestRegisterModel } from 'src/app/shared/models/RequestRegisterModel';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends BaseService {
  // Observable navItem source
  private authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this.authNavStatusSource.asObservable();

  private manager = new UserManager(getClientSettings());
  private user: User | null;

  constructor(private http: HttpClient, private configService: ConfigService) {
    super();

    this.manager.getUser().then((user) => {
      this.user = user;
      this.authNavStatusSource.next(this.isAuthenticated());
    });
  }

  getUserData() {
    this.manager.getUser().then((user) => {
      this.user = user;
      console.log(this.user);
      this.authNavStatusSource.next(this.isAuthenticated());
    });
  }

  login() {
    return this.manager.signinRedirect();
  }

  async completeAuthentication() {
    this.user = await this.manager.signinRedirectCallback();
    this.authNavStatusSource.next(this.isAuthenticated());
  }

  register(data: RequestRegisterModel) {
    return this.http
      .post(this.configService.authApiURI + '/account/Register', data)
      .pipe(catchError(this.handleError));
  }

  isAuthenticated(): boolean {
    return this.user != null && !this.user.expired;
  }

  get authorizationHeaderValue(): string {
    return `${this.user.token_type} ${this.user.access_token}`;
  }

  get name(): string {
    return this.user != null ? this.user.profile.name : '';
  }

  async signout() {
    await this.manager.signoutRedirect();
  }
}

export function getClientSettings(): UserManagerSettings {
  return {
    authority: 'https://localhost:5001',
    client_id: 'angular_spa',
    redirect_uri: 'http://localhost:4200/auth-callback',
    response_type: 'code',
    scope: 'openid profile api1',
    post_logout_redirect_uri: 'http://localhost:4200/',
    // response_type: 'id_token token',
    // filterProtocolClaims: true,
    // loadUserInfo: true,
    // automaticSilentRenew: true,
    // silent_redirect_uri: 'http://localhost:4200/silent-refresh.html',
  };
}
