import type { Course, CourseIndex, Id } from '../models';
import HttpServiceV1 from './HttpServiceV1';
import VideoService from './VideoService';

export default class CourseService {
  static async create(course: Course, token: string | null | undefined = null): Promise<Course> {
    return await HttpServiceV1.post<Course>('course', course, token);
  }

  static async delete(id: Id, token: string | null | undefined = null): Promise<Course> {
    return await HttpServiceV1.delete<Course>('course', id, token);
  }

  static async fillTemplate(template: string, token: string | null | undefined): Promise<string> {
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
        presignedUrls.set(textToReplace, await VideoService.getPresignedUrl(filePath, token));
      }
    }
    for (let [key, value] of presignedUrls) {
      template = template.replace(key, value);
    }
    return template;
  }

  static async get(id: Id, token: string | null | undefined): Promise<Course> {
    const course = await HttpServiceV1.get<Course>('course', id, token);
    course.templatedContent = await CourseService.fillTemplate(course.content, token);
    return course;
  }

  static async getAll(token: string | null | undefined): Promise<CourseIndex> {
    return await HttpServiceV1.getAll<CourseIndex>('course', token);
  }

  static async update(course: Course, token: string | null | undefined = null): Promise<Course> {
    return await HttpServiceV1.put<Course>('course', course, token);
  }
};
