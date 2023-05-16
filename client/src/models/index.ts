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
};

export interface CourseMetadata {
  description: string
  name: string
};

export type UserInfo = {
  sub: string,
  nickname: string,
  name: string,x
  picture: string,
  updated_at: string,
  email: string,
  email_verified: string,
};
