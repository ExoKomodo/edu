type UrlSuffix = string | null | undefined;

export default class HttpServiceV1 {
  static baseUrl: string = process.env.NODE_ENV == 'production' ? 'https://services.edu.exokomodo.com/api/v1' : 'http://localhost:5000/api/v1';
  static httpsScheme: string = 'https://';
  static auth0BaseUrl: string = 'exokomodo.us.auth0.com';

  static constructUrl(urlSuffix: UrlSuffix): string {
    let url = `${httpsScheme}${HttpServiceV1.baseUrl}/`;
    if (urlSuffix) {
      url += `${urlSuffix}`;
    }
    return url;
  }

  static async delete<T>(urlSuffix: UrlSuffix, id: string, token: string | null | undefined): Promise<T> {
    const headers: undefined | {Authorization: string} = token ? {
      Authorization: `Bearer ${token}`
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

  static async get<T>(urlSuffix: UrlSuffix, id: string, token: string | null | undefined): Promise<T> {
    const headers: undefined | {Authorization: string} = token ? {
      Authorization: `Bearer ${token}`
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

  static async getAll<T>(urlSuffix: UrlSuffix, token: string | null | undefined): Promise<T> {
    const headers: undefined | {Authorization: string} = token ? {
      Authorization: `Bearer ${token}`
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

  static async post<T>(urlSuffix: UrlSuffix, value: any, token: string | null | undefined): Promise<T> {
    const headers: undefined | {Authorization: string} = token ? {
      Authorization: `Bearer ${token}`
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

  static async put<T>(urlSuffix: UrlSuffix, value: any, token: string | null | undefined): Promise<T> {
    const headers: undefined | {Authorization: string} = token ? {
      Authorization: `Bearer ${token}`
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

  static async rawGet<T>(url: string, token: string | null | undefined): Promise<T> {
    const headers: undefined | {Authorization: string} = token ? {
      Authorization: `Bearer ${token}`
    } : undefined;
    const response = await fetch(
      `${httpsScheme}${url}`,
      {
        method: 'GET',
        headers: headers,
      },
    );
    return await response.json() as T;
  }
};
