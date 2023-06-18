import BlobService from './BlobService';
import HttpServiceV1, { type HttpOptions } from './HttpServiceV1';
import type { Assignment, AssignmentIndex, Id } from '@/models';

export default class AssignmentService {
  static async createAsync(assignment: Assignment, options: HttpOptions={}): Promise<Assignment> {
    try {
      return await HttpServiceV1.postAsync<Assignment>('assignment', assignment, options);
    }
    catch (err: any) {
      options.toast?.error(`Failed to create assignment: ${err}`);
      throw err;
    }
  }

  static async deleteAsync(id: Id, options: HttpOptions={}): Promise<Assignment> {
    try {
      return await HttpServiceV1.deleteAsync<Assignment>('assignment', id, options);
    }
    catch (err: any) {
      options.toast?.error(`Failed to delete assignment: ${err}`);
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

  static async getAsync(id: Id, options: HttpOptions={}): Promise<Assignment> {
    try {
      return await HttpServiceV1.getAsync<Assignment>('assignment', id, options);
    }
    catch (err: any) {
      options.toast?.error(`Failed to get assignment: ${err}`);
      throw err;
    }
  }

  static async getAllAsync(options: HttpOptions={}): Promise<AssignmentIndex> {
    try {
      return await HttpServiceV1.getAllAsync<AssignmentIndex>('assignment', options);
    }
    catch (err: any) {
      options.toast?.error(`Failed to get all assignments: ${err}`);
      throw err;
    }
  }

  static async updateAsync(assignment: Assignment, options: HttpOptions={}): Promise<Assignment> {
    try {
      return await HttpServiceV1.putAsync<Assignment>('assignment', assignment, options);
    }
    catch (err: any) {
      options.toast?.error(`Failed to update assignment: ${err}`);
      throw err;
    }
  }
};
