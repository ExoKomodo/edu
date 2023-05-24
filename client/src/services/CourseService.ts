import BlobService from './BlobService';
import HttpServiceV1, { type HttpOptions } from './HttpServiceV1';
import type { Course, CourseIndex, Id } from '@/models';

export default class CourseService {
  static async createAsync(course: Course, options: HttpOptions={}): Promise<Course> {
    try {
      return await HttpServiceV1.postAsync<Course>('course', course, options);
    }
    catch (err: any) {
      options.toast?.error(`Failed to create course: ${err}`);
      throw err;
    }
  }

  static async deleteAsync(id: Id, options: HttpOptions={}): Promise<Course> {
    try {
      return await HttpServiceV1.deleteAsync<Course>('course', id, options);
    }
    catch (err: any) {
      options.toast?.error(`Failed to delete course: ${err}`);
      throw err;
    }
  }

  static async fillTemplateAsync(template: string, options: HttpOptions={}): Promise<string> {
    if (!template) {
      return '';
    }
    // NOTE: Match and captures what is between ${}, to replace with presigned URLss
    const re = /"\${([0-9a-zA-Z_\-\/\.]+)}"/g;
    const presignedUrls = new Map<string, string>();
    for (let match of template.matchAll(re)) {
      const textToReplace = match[0];
      const filePath = match[1];
      if (!(textToReplace in presignedUrls)) {
        presignedUrls.set(textToReplace, await BlobService.getPresignedUrlAsync(filePath, options));
      }
    }
    for (let [key, value] of presignedUrls) {
      template = template.replace(key, value);
    }
    return template;
  }

  static async getAsync(id: Id, options: HttpOptions={}): Promise<Course> {
    try {
      const course = await HttpServiceV1.getAsync<Course>('course', id, options);
      try {
        course.templatedContent = await CourseService.fillTemplateAsync(course.content, options);
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

  static async getAllAsync(options: HttpOptions={}): Promise<CourseIndex> {
    try {
      return await HttpServiceV1.getAllAsync<CourseIndex>('course', options);
    }
    catch (err: any) {
      options.toast?.error(`Failed to get all courses: ${err}`);
      throw err;
    }
  }

  static async updateAsync(course: Course, options: HttpOptions={}): Promise<Course> {
    try {
      return await HttpServiceV1.putAsync<Course>('course', course, options);
    }
    catch (err: any) {
      options.toast?.error(`Failed to update course: ${err}`);
      throw err;
    }
  }
};
