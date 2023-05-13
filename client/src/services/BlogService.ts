import type { Blog, BlogIndex, Id } from '../models';
import HttpServiceV1 from './HttpServiceV1';

export default class BlogService {
  static async get(id: Id, token: string | null | undefined): Promise<Blog> {
    return await HttpServiceV1.get<Blog>('blog', id, token);
  }

  static async getAll(token: string | null | undefined): Promise<BlogIndex> {
    return await HttpServiceV1.getAll<BlogIndex>('blog', token);
  }
};
