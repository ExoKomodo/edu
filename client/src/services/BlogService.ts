import type { Blog } from '../models';

export default class BlogService {
  static get(id: string): Blog {
    return {
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
    }[id] ?? {
      id: '-420',
      description: 'Oh no',
      title: 'Oh no',
      content: 'Whoopsie! You got a non-existent blog!',
    };
  }
};
