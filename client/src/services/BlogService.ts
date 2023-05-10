import type { Blog, BlogIndex, Id } from '../models';
import HttpServiceV1 from './HttpServiceV1';

export default class BlogService {
  static async get(id: Id): Promise<Blog> {
    return await HttpServiceV1.get<Blog>('blog', id);
  }

  static async getAll(): Promise<BlogIndex> {
    return await HttpServiceV1.getAll<BlogIndex>('blog');
  }
};
