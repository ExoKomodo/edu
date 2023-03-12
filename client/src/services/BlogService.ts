import type { Blog, Id } from '../models';

export default class BlogService {
  static get(id: Id): Blog {
    // TODO: Use fetch()
  }

  static getMany(ids: Id[]): Blog[] {
    return ids.map(BlogService.get);
  }

  static getStub(id: Id): Blog {
    // TODO: Use fetch()
    
  }

  static getManyStubs(ids: Id[]): Blog[] {
    return ids.map(BlogService.getStub);
  }
};
