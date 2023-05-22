import HttpServiceV1 from './HttpServiceV1';
import type { Blog, BlogIndex, Id } from '@/models';

export default class BlogService {
  static async get(id: Id, token: string | null | undefined = undefined): Promise<Blog> {
    return await HttpServiceV1.get<Blog>('blog', id, token);
  }

  static async getAll(token: string | null | undefined = undefined): Promise<BlogIndex> {
    return await HttpServiceV1.getAll<BlogIndex>('blog', token);
  }
};
