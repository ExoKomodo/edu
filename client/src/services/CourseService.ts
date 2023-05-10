import type { Course, CourseIndex, Id } from '../models';
import HttpServiceV1 from './HttpServiceV1';

export default class CourseService {
  static async get(id: Id): Promise<Course> {
    return await HttpServiceV1.get<Course>('course', id);
  }

  static async getAll(): Promise<CourseIndex> {
    return await HttpServiceV1.getAll<CourseIndex>('course');
  }
};
