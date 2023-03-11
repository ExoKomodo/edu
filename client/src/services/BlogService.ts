import type { Blog, Id } from '../models';

export default class BlogService {
  private static store: Record<Id, Blog> = {
    '1': {
      id: '1',
      description: 'A short first post',
      title: 'I am a blog',
      content: 'Lorem ipsum 1',
    },
    '2': {
      id: '2',
      description: 'A longer second post',
      title: 'a blog but again',
      content: 'Lorem ipsum 2',
    },
    '3': {
      id: '3',
      description: 'I cannot believe I am still doing this',
      title: 'One for each person of the trinity',
      content: 'Lorem ipsum 3',
    },
  };

  private static emptyBlog: Blog = {
    id: '-420',
    description: 'Oh no',
    title: 'Oh no',
    content: 'Whoopsie! You got a non-existent blog!',
  };

  static get(id: Id): Blog {
    return this.store[id] ?? this.emptyBlog;
  }

  static getMany(ids: Id[]): Blog[] {
    return ids.map(BlogService.get);
  }

  static getStub(id: Id): Blog {
    const blogStub = (this.store[id] ?? this.emptyBlog);
    blogStub.content = undefined;
    return blogStub;
  }

  static getManyStubs(ids: Id[]): Blog[] {
    return ids.map(BlogService.getStub);
  }
};
