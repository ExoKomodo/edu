import type { Blog, BlogIndex, Id } from '../models';

export default class BlogService {
  static baseUrl: string = process.env.NODE_ENV == 'production' ? 'https://services.edu.exokomodo.com/api/v1/blog' : 'http://localhost:5000/api/v1/blog';

  static async get(id: Id): Promise<Blog> {
    const response = await fetch(
      `${BlogService.baseUrl}/${id}`,
      {
        method: 'GET',
      },
    );
    return await response.json() as Blog;
  }

  static async getAll(): Promise<BlogIndex> {
    const response = await fetch(
      BlogService.baseUrl,
      {
        method: 'GET',
      },
    );
    return await response.json() as BlogIndex;
  }
};
