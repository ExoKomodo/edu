import HttpServiceV1 from './HttpServiceV1';
import type { Auth0VueClient } from '@auth0/auth0-vue';
import type { UserInfo } from '@/models';

export default class AuthService {
  static async getAccessTokenAsync(auth0: Auth0VueClient): Promise<string | null> {
    return auth0.getAccessTokenSilently();
  }

  static async getUserInfoAsync(auth0: Auth0VueClient, token: string | null | undefined = undefined): Promise<UserInfo> {
    return await HttpServiceV1.get<UserInfo>(
      'user',
      'info',
      token ? token : await AuthService.getAccessTokenAsync(auth0),
    );
  }

  static isAdmin(auth0: Auth0VueClient): boolean {
    return auth0.isAuthenticated && [
      "exokomodo@gmail.com",
      "brandonapol@cedarville.edu",
    ].includes(auth0.user.value.email ?? '');
  }

  static login(auth0: Auth0VueClient): void {
    auth0.loginWithRedirect();
  };

  static logout(auth0: Auth0VueClient): void {
    auth0.logout(
      {
        logoutParams: {
          returnTo: window.location.origin,
        },
      },
    );
  };
};
