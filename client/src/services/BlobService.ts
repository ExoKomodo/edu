import type { Id } from '@/models';
import HttpServiceV1, { type HttpOptions } from './HttpServiceV1';
import type { ToastInterface } from 'vue-toastification';

export default class BlobService {
  static async getPresignedUrlAsync(id: Id, options: HttpOptions = {}): Promise<string> {
    try {
      return await HttpServiceV1.getTextAsync(`blob?url=${id}`, options);
    }
    catch (err: any) {
      options.toast?.error(`Failed to get blob's presigned url: ${err}`);
      throw err;
    }
  }
};
