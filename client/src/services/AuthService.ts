import HttpServiceV1, { type HttpOptions } from './HttpServiceV1';
import type { Auth0VueClient } from '@auth0/auth0-vue';
import type { UserInfo } from '@/models';

export default class AuthService {
  static async getAccessTokenAsync(auth0: Auth0VueClient, options: HttpOptions={}): Promise<string | null> {
    return auth0.getAccessTokenSilently();
  }

  static async getUserInfoAsync(auth0: Auth0VueClient, options: HttpOptions={}): Promise<UserInfo> {
    try {
      return await HttpServiceV1.getAsync<UserInfo>(
        'user',
        'info',
        options,
      );
    }
    catch (err: any) {
      console.error(err);
      options.toast?.error(`Failed to get user info: ${err}`);
      throw err;
    }
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
