import type { Course, CourseIndex, Id } from '../models';
import HttpServiceV1 from './HttpServiceV1';

export default class CourseService {
  static async create(course: Course, token: string | null | undefined=null): Promise<Course> {
    return await HttpServiceV1.post<Course>('course', course, token);
  }

  static async get(id: Id, token: string | null | undefined=null): Promise<Course> {
    return await HttpServiceV1.get<Course>('course', id, token);
  }

  static async getAll(token: string | null | undefined): Promise<CourseIndex> {
    return await HttpServiceV1.getAll<CourseIndex>('course', token);
  }

  static async update(course: Course, token: string | null | undefined=null): Promise<Course> {
    return await HttpServiceV1.put<Course>('course', course, token);
  }
};
