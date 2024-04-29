import type { BlogIndex, Id, ViewKey } from '@/models';
import { Blog, BlogMetadata } from '@/models';
import HttpServiceV1, { type HttpOptions } from './HttpServiceV1';

export default class BlogService {
  objectViewKeys: ViewKey[] = [
    { key: 'id', kind: 'text' },
  ];

  make(): Blog {
    return new Blog({});
  }

  static async getAsync(id: Id, options: HttpOptions = {}): Promise<Blog> {
    try {
      return new Blog(
        await HttpServiceV1.getAsync('blog', id, options));
    }
    catch (err: any) {
      options.toast?.error(`Failed to get blog: ${err}`);
      throw err;
    }
  }

  static async getAllAsync(options: HttpOptions = {}): Promise<BlogIndex> {
    try {
      const index = await HttpServiceV1.getAllAsync('blog', options) as any;
      Object.keys(index.blogs).forEach(function(key, _) {
        index.blogs[key] = new BlogMetadata(index.blogs[key]);
      });
      index.blogs = new Map<Id, BlogMetadata>(Object.entries(index.blogs));
      return index;
    }
    catch (err: any) {
      options.toast?.error(`Failed to get all blogs: ${err}`);
      throw err;
    }
  }
};
