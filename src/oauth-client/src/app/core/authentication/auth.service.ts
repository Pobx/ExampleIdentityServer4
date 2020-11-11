import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserManager, UserManagerSettings, User } from 'oidc-client';
import { BehaviorSubject, Observable } from 'rxjs';
import { RequestRegisterModel } from 'src/app/shared/models/RequestRegisterModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private api: string;
  private authNavStatusSource = new BehaviorSubject<boolean>(false);
  publicauthNavStatus$: Observable<boolean> = this.authNavStatusSource;

  public manager = new UserManager(getClientSettings());
  private user: User | null;

  constructor(private http: HttpClient) {
    this.api = environment.authApiURI;
    this.manager.getUser().then((user) => {
      this.user = user;
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
    return this.http.post(`${this.api}/account/Register`, data);
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
    response_type: 'id_token token',
    scope: 'openid profile api1',
    post_logout_redirect_uri: 'http://localhost:4200/login',
  };
}
