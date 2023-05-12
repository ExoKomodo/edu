export type Id = string;

export interface Blog {
  id: Id
  content: string
  metadata: BlogMetadata
};

export interface BlogIndex {
  blogs: Map<Id, BlogMetadata>
}

export interface BlogMetadata {
  description: string
  title: string
}

export interface Course {
  id: Id
  content: string
  metadata: CourseMetadata
};

export type CourseIndex = Map<Id, CourseMetadata>;

export interface CourseMetadata {
  description: string
  name: string
}
