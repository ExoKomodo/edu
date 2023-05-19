type UrlPortion = string | null | undefined;

export default class HttpServiceV1 {
  static baseOrigin: string = process.env.NODE_ENV == 'production' ? 'https://services.edu.exokomodo.com' : 'http://localhost:80';
  static baseUrl: string = `${HttpServiceV1.baseOrigin}/api/v1`;
  static auth0UrlScheme: string = 'https://';
  static auth0BaseUrl: string = 'exokomodo.us.auth0.com';

  static constructUrl(urlSuffix: UrlPortion): string {
    let url = `${HttpServiceV1.baseUrl}/`;
    if (urlSuffix) {
      url += `${urlSuffix}`;
    }
    return url;
  }

  static async delete<T>(urlSuffix: UrlPortion, id: string, token: string | null | undefined): Promise<T> {
    const headers: undefined | { Authorization: string, Origin: string } = token ? {
      Authorization: `Bearer ${token}`,
      Origin: window.location.origin,
    } : undefined;
    const response = await fetch(
      `${HttpServiceV1.constructUrl(urlSuffix)}/${id}`,
      {
        method: 'DELETE',
        headers: headers,
      },
    );
    return await response.json() as T;
  }

  static async get<T>(urlSuffix: UrlPortion, id: string, token: string | null | undefined): Promise<T> {
    const headers: undefined | { Authorization: string, Origin: string } = token ? {
      Authorization: `Bearer ${token}`,
      Origin: window.location.origin,
    } : undefined;
    const response = await fetch(
      `${HttpServiceV1.constructUrl(urlSuffix)}/${id}`,
      {
        method: 'GET',
        headers: headers,
      },
    );
    return await response.json() as T;
  }

  static async getAll<T>(urlSuffix: UrlPortion, token: string | null | undefined): Promise<T> {
    const headers: undefined | { Authorization: string, Origin: string } = token ? {
      Authorization: `Bearer ${token}`,
      Origin: window.location.origin,
    } : undefined;
    const response = await fetch(
      `${HttpServiceV1.constructUrl(urlSuffix)}`,
      {
        method: 'GET',
        headers: headers,
      },
    );
    return await response.json() as T;
  }

  static async post<T>(urlSuffix: UrlPortion, value: any, token: string | null | undefined): Promise<T> {
    const headers: undefined | { Authorization: string, Origin: string } = token ? {
      Authorization: `Bearer ${token}`,
      Origin: window.location.origin,
    } : undefined;
    const response = await fetch(
      `${HttpServiceV1.constructUrl(urlSuffix)}`,
      {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(value),
      },
    );
    return await response.json() as T;
  }

  static async put<T>(urlSuffix: UrlPortion, value: any, token: string | null | undefined): Promise<T> {
    const headers: undefined | { Authorization: string, Origin: string } = token ? {
      Authorization: `Bearer ${token}`,
      Origin: window.location.origin,
    } : undefined;
    const response = await fetch(
      `${HttpServiceV1.constructUrl(urlSuffix)}`,
      {
        method: 'PUT',
        headers: headers,
        body: JSON.stringify(value),
      },
    );
    return await response.json() as T;
  }

  static async getText(url: string, token: string | null | undefined): Promise<string> {
    const headers: undefined | { Authorization: string, Origin: string } = token ? {
      Authorization: `Bearer ${token}`,
      Origin: window.location.origin,
    } : undefined;
    const response = await fetch(
      HttpServiceV1.constructUrl(url),
      {
        method: 'GET',
        headers: headers,
      },
    );
    return await response.text();
  }
};
