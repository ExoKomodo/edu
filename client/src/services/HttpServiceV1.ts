import type { ToastInterface } from "vue-toastification";

type UrlPortion = string | null | undefined;

export type HttpOptions = {
  toast?: ToastInterface,
  token?: string | null | undefined,
};

type HttpHeaders = {
  Authorization?: string,
  Origin?: string
};

export default class HttpServiceV1 {
  static baseOrigin: string = process.env.NODE_ENV == 'production' ? 'https://services.edu.exokomodo.com' : 'http://localhost:5000';
  static baseUrl: string = `${HttpServiceV1.baseOrigin}/api/v1`;
  static auth0UrlScheme: string = 'https://';
  static auth0BaseUrl: string = 'exokomodo.us.auth0.com';

  private static getDefaultHeaders(options: HttpOptions): HttpHeaders {
    return options.token ? {
      Authorization: `Bearer ${options.token}`,
      Origin: window.location.origin,
    } : {};
  }

  static constructUrl(urlSuffix: UrlPortion): string {
    let url = `${HttpServiceV1.baseUrl}/`;
    if (urlSuffix) {
      url += `${urlSuffix}`;
    }
    return url;
  }

  static async deleteAsync<T>(urlSuffix: UrlPortion, id: string, options: HttpOptions={}): Promise<T> {
    const headers = HttpServiceV1.getDefaultHeaders(options);
    const response = await fetch(
      `${HttpServiceV1.constructUrl(urlSuffix)}/${id}`,
      {
        method: 'DELETE',
        headers: headers,
      },
    );
    return await response.json() as T;
  }

  static async getAsync<T>(urlSuffix: UrlPortion, id: string, options: HttpOptions={}): Promise<T> {
    const headers = HttpServiceV1.getDefaultHeaders(options);
    const response = await fetch(
      `${HttpServiceV1.constructUrl(urlSuffix)}/${id}`,
      {
        method: 'GET',
        headers: headers,
      },
    );
    return await response.json() as T;
  }

  static async getAllAsync<T>(urlSuffix: UrlPortion, options: HttpOptions={}): Promise<T> {
    const headers = HttpServiceV1.getDefaultHeaders(options);
    const response = await fetch(
      `${HttpServiceV1.constructUrl(urlSuffix)}`,
      {
        method: 'GET',
        headers: headers,
      },
    );
    return await response.json() as T;
  }

  static async postAsync<T>(urlSuffix: UrlPortion, value: any, options: HttpOptions={}): Promise<T> {
    const headers = HttpServiceV1.getDefaultHeaders(options);
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

  static async putAsync<T>(urlSuffix: UrlPortion, value: any, options: HttpOptions={}): Promise<T> {
    const headers = HttpServiceV1.getDefaultHeaders(options);
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

  static async getTextAsync(url: string, options: HttpOptions={}): Promise<string> {
    const headers = HttpServiceV1.getDefaultHeaders(options);
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
