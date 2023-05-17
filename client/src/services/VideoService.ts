import type { Id } from '@/models';
import HttpServiceV1 from './HttpServiceV1';

export default class VideoService {  
  static async getPresignedUrl(id: Id, token: string | null | undefined=null): Promise<string> {
    return await HttpServiceV1.getText(`video/url/${id}`, token);
  }
};
