import BlobService from './BlobService';
import HttpServiceV1, { type HttpOptions } from './HttpServiceV1';
import { Assignment, AssignmentMetadata, type AssignmentIndex, type Id, type ViewKey } from '@/models';
import type ModelService from './ModelService';

export default class AssignmentService implements ModelService<Assignment>  {
  objectViewKeys: ViewKey[] = [
    { key: 'id', kind: 'text' },
  ];

  make(): Assignment {
    return new Assignment({});
  }

  static async createAsync(assignment: Assignment, options: HttpOptions = {}): Promise<Assignment> {
    try {
      return new Assignment(
        await HttpServiceV1.postAsync('assignment', assignment, options));
      return new Assignment(
        await HttpServiceV1.postAsync('assignment', assignment, options));
    }
    catch (err: any) {
      options.toast?.error(`Failed to create assignment: ${err}`);
      throw err;
    }
  }

  static async deleteAsync(id: Id, options: HttpOptions = {}): Promise<Assignment> {
    try {
      return new Assignment(
        await HttpServiceV1.deleteAsync('assignment', id, options));
      return new Assignment(
        await HttpServiceV1.deleteAsync('assignment', id, options));
    }
    catch (err: any) {
      options.toast?.error(`Failed to delete assignment: ${err}`);
      throw err;
    }
  }

  static async fillTemplateAsync(template: string, options: HttpOptions = {}): Promise<string> {
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

  static async getAsync(id: Id, options: HttpOptions = {}): Promise<Assignment> {
    try {
      return new Assignment(
        await HttpServiceV1.getAsync('assignment', id, options));
      return new Assignment(
        await HttpServiceV1.getAsync('assignment', id, options));
    }
    catch (err: any) {
      options.toast?.error(`Failed to get assignment: ${err}`);
      throw err;
    }
  }

  static async getAllAsync(options: HttpOptions = {}): Promise<AssignmentIndex> {
    try {
      let index = await HttpServiceV1.getAllAsync('assignment', options) as any;
      Object.keys(index).forEach(function(key, _) {
        index[key] = new AssignmentMetadata(index[key]);
      });
      index = new Map<Id, AssignmentMetadata>(Object.entries(index));
      return index;
      let index = await HttpServiceV1.getAllAsync('assignment', options) as any;
      Object.keys(index).forEach(function(key, _) {
        index[key] = new AssignmentMetadata(index[key]);
      });
      index = new Map<Id, AssignmentMetadata>(Object.entries(index));
      return index;
    }
    catch (err: any) {
      options.toast?.error(`Failed to get all assignments: ${err}`);
      throw err;
    }
  }

  static async updateAsync(assignment: Assignment, options: HttpOptions = {}): Promise<Assignment> {
    try {
      return new Assignment(
        await HttpServiceV1.putAsync('assignment', assignment, options));
      return new Assignment(
        await HttpServiceV1.putAsync('assignment', assignment, options));
    }
    catch (err: any) {
      options.toast?.error(`Failed to update assignment: ${err}`);
      throw err;
    }
  }
};
