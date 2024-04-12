import HttpServiceV1, { type HttpOptions } from './HttpServiceV1';
import type { Auth0VueClient, User } from '@auth0/auth0-vue';
import type { UserInfo } from '@/models';
import type { Ref } from 'vue';

export default class AuthService {
  static async getAccessTokenAsync(auth0: Auth0VueClient, options: HttpOptions = {}): Promise<string | null> {
    return auth0.getAccessTokenSilently();
  }

  static async getUserInfoAsync(auth0: Auth0VueClient, options: HttpOptions = {}): Promise<UserInfo> {
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
    if (auth0.user.value === null || !auth0.isAuthenticated) {
      return false;
    }
    const user: Ref<User> = auth0.user as Ref<User>;
    return [
      "exokomodo@gmail.com",
      "brandonapol@cedarville.edu",
    ].includes(user.value.email ?? '');
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
