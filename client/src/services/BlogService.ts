import type { Blog, Id } from '../models';

export default class BlogService {
  static baseUrl: string = process.env.NODE_ENV == 'production' ? 'https://services.edu.exokomodo.com/api/v1/blog' : 'http://localhost:5000/api/v1/blog';

  static async get(id: Id): Promise<Blog> {
    const response = await fetch(`${BlogService.baseUrl}/${id}`);
    return await response.json() as Blog;
  }

  static async getAll(): Promise<Blog[]> {
    const response = await fetch(BlogService.baseUrl);
    return await response.json() as Blog[];
  }

  static async getMany(ids: Id[]): Promise<Blog[]> {
    return await Promise.all(
      ids.map(id => BlogService.get(id))
    );
  }

  static async getStub(id: Id): Promise<Blog> {
    const blog = await BlogService.get(id);
    blog.content = undefined;
    return blog;
  }

  static async getAllStubs(): Promise<Blog[]> {
    const blogs = await BlogService.getAll();
    for (let blog of blogs) {
      blog.content = undefined;
    }
    return blogs;
  }

  static async getManyStubs(ids: Id[]): Promise<Blog[]> {
    return await Promise.all(
      ids.map(id => BlogService.getStub(id))
    );
  }
};
