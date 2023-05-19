export type Id = string;

export interface Blog {
  id: Id
  content: string
  metadata: BlogMetadata
};

export interface BlogIndex {
  blogs: Map<Id, BlogMetadata>
};

export interface BlogMetadata {
  description: string
  title: string
};

export interface Course {
  id: Id
  content: string
  metadata: CourseMetadata
  templatedContent?: string
};

export type CourseIndex = Map<Id, CourseMetadata>;

export interface CourseMetadata {
  description: string
  name: string
};

export type UserInfo = {
  sub: string,
  nickname: string,
  name: string,
  picture: string,
  updated_at: string,
  email: string,
  email_verified: string,
};
