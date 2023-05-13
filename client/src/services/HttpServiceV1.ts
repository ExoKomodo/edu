type UrlSuffix = string | null | undefined;

export default class HttpServiceV1 {
  static baseUrl: string = process.env.NODE_ENV == 'production' ? 'https://services.edu.exokomodo.com/api/v1' : 'http://localhost:5000/api/v1';

  static constructUrl(urlSuffix: UrlSuffix): string {
    let url = `${HttpServiceV1.baseUrl}/`;
    if (urlSuffix) {
      url += `${urlSuffix}`;
    }
    return url;
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
};
