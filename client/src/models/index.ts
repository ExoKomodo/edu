export type Id = string;
export type InputKind = 'text' | 'number' | 'code';

export type ViewKey = {
  key: string
  kind: InputKind
  height?: number
}

export abstract class ViewModel {
  id: Id;
  
  static make<T extends ViewModel>(c: new () => T): T {
    return new c();
  }
  constructor(id: Id) {
    this.id = id;
  }
}

export class Blog extends ViewModel {
  content: string
  metadata: BlogMetadata
  constructor({id = '', content = '', metadata = new BlogMetadata({})} ) {
    super(id);
    this.content = content;
    this.metadata = metadata;
  }
};

export interface BlogIndex {
  blogs: Map<Id, BlogMetadata>
};

export class BlogMetadata {
  description: string
  title: string
  constructor({description = '', title = ''}) {
    this.description = description;
    this.title = title;
  }
};

export class Assignment extends ViewModel {
  problemExplanation: string
  metadata: AssignmentMetadata
  constructor({id = '', problemExplanation = '', metadata = new AssignmentMetadata({})}) {
    super(id);
    this.problemExplanation = problemExplanation;
    this.metadata = metadata;
  }
};

export type AssignmentIndex = Map<Id, AssignmentMetadata>;

export class AssignmentMetadata {
  description: string
  name: string
  requiredSectionIds: Id[]
  courseId: Id
  constructor({description = '', name = '', requiredSectionIds = [], courseId = ''}) {
    this.description = description;
    this.name = name;
    this.requiredSectionIds = requiredSectionIds;
    this.courseId = courseId;
  }
};

export class Course extends ViewModel {
  content: string
  metadata: CourseMetadata
  templatedContent?: string

  constructor({id = '', content = '', metadata = new CourseMetadata({})}) {
    super(id);
    this.content = content;
    this.metadata = metadata;
  }
};

export class CourseEditorState {
  isEditMode: boolean = false
  showPreview: boolean = false
  id: string = ''
  name: string = ''
  description: string = ''
  content: string = ''
};

export type CourseIndex = Map<Id, CourseMetadata>;

export class CourseMetadata {
  description: string
  name: string
  constructor({description = '', name = ''}) {
    this.description = description;
    this.name = name;
  }
};

export class Section extends ViewModel {
  content: string
  difficulty: number
  metadata: SectionMetadata

  constructor({id = '', content = '', difficulty = 0, metadata = new SectionMetadata({})}) {
    super(id);
    this.content = content;
    this.difficulty = difficulty;
    this.metadata = metadata;
  }
};

export type SectionIndex = Map<Id, SectionMetadata>;

export class SectionMetadata {
  description: string
  name: string
  courseId: Id
  constructor({description = '', name = '', courseId = ''}) {
    this.description = description;
    this.name = name;
    this.courseId = courseId;
  }
};

export class UserInfo {
  sub: string
  nickname: string
  name: string
  picture: string
  updated_at: string
  email: string
  email_verified: string
  constructor({sub = '', nickname = '', name = '', picture = '', updated_at = '', email = '', email_verified = ''}) {
    this.sub = sub;
    this.nickname = nickname;
    this.name = name;
    this.picture = picture;
    this.updated_at = updated_at;
    this.email = email;
    this.email_verified = email_verified;
  }
};
