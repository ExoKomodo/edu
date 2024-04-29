import { Course, CourseMetadata, type CourseIndex, type Id, type ViewKey } from '@/models';
import HttpServiceV1, { type HttpOptions } from './HttpServiceV1';
import ModelService from './ModelService';

export default class CourseService implements ModelService<Course> {
  objectViewKeys: ViewKey[] = [
    { key: 'id', kind: 'text' },
    { key: 'name', kind: 'code' },
    { key: 'description', kind: 'code' },
    { key: 'content', kind: 'code' },
  ];
  
  make(): Course {
    return new Course({});
  }

  static async createAsync(course: Course, options: HttpOptions = {}): Promise<Course> {
    try {
      return new Course(
        await HttpServiceV1.postAsync('course', course, options));
    }
    catch (err: any) {
      options.toast?.error(`Failed to create course: ${err}`);
      throw err;
    }
  }

  static async deleteAsync(id: Id, options: HttpOptions = {}): Promise<Course> {
    try {
      return new Course(
        await HttpServiceV1.deleteAsync('course', id, options));
    }
    catch (err: any) {
      options.toast?.error(`Failed to delete course: ${err}`);
      throw err;
    }
  }
  static async getAsync(id: Id, options: HttpOptions = {}): Promise<Course> {
    try {
      const course = new Course(
        await HttpServiceV1.getAsync('course', id, options));
      try {
        course.templatedContent = await ModelService.fillTemplateAsync(course.content, options);
      }
      catch (err: any) {
        options.toast?.error(`Failed to fill template for course content: ${err}`);
        throw err;
      }
      return course;
    }
    catch (err: any) {
      options.toast?.error(`Failed to get course: ${err}`);
      throw err;
    }
  }

  static async getAllAsync(options: HttpOptions = {}): Promise<CourseIndex> {
    try {
      const index = await HttpServiceV1.getAllAsync('course', options) as any;
      Object.keys(index).forEach(function(key, _) {
        index[key] = new CourseMetadata(index[key]);
      });
      return new Map<Id, CourseMetadata>(Object.entries(index));
    }
    catch (err: any) {
      options.toast?.error(`Failed to get all courses: ${err}`);
      throw err;
    }
  }

  static async updateAsync(course: Course, options: HttpOptions = {}): Promise<Course> {
    try {
      return new Course(
        await HttpServiceV1.putAsync('course', course, options));
    }
    catch (err: any) {
      options.toast?.error(`Failed to update course: ${err}`);
      throw err;
    }
  }
};
