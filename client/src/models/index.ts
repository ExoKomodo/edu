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

export interface Assignment {
  id: Id
  problemExplanation: string
  metadata: AssignmentMetadata
};

export type AssignmentIndex = Map<Id, AssignmentMetadata>;

export interface AssignmentMetadata {
  description: string
  name: string
  requiredSectionIds: Id[]
  courseId: Id
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

export interface Section {
  id: Id
  content: string
  difficulty: number
  metadata: SectionMetadata
};

export type SectionIndex = Map<Id, SectionMetadata>;

export interface SectionMetadata {
  description: string
  name: string
  courseId: Id
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
