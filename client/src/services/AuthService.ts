import type { Auth0VueClient } from '@auth0/auth0-vue';

export default class AuthService {
  static getAccessToken(auth0: Auth0VueClient): string | null {
    let token: string | null = null
    AuthService.getAccessTokenAsync(auth0).then(value => {
      token = value;
    }).catch(err => {
      console.error(err);
    });
    return token;
  }
  
  static async getAccessTokenAsync(auth0: Auth0VueClient): Promise<string | null> {
    return auth0.getAccessTokenSilently();
  }

  static login(auth0: Auth0VueClient) {
    auth0.loginWithRedirect();
    const accessToken = AuthService.getAccessToken(auth0);
    if (accessToken) {
      localStorage.setItem('accessToken', accessToken);
    }
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
