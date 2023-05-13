import type { Auth0VueClient } from '@auth0/auth0-vue';

export default class AuthService {
  static async getAccessTokenAsync(auth0: Auth0VueClient): Promise<string | null> {
    return auth0.getAccessTokenSilently();
  }

  static login(auth0: Auth0VueClient) {
    auth0.loginWithRedirect();
  };
  
  static logout(auth0: Auth0VueClient) {
    auth0.logout(
      {
        logoutParams: {
          returnTo: window.location.origin,
        },
      },
    );
  };
};
