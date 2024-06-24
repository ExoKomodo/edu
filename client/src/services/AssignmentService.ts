import HttpServiceV1, { type HttpOptions } from './HttpServiceV1';
import { Assignment, AssignmentMetadata, type AssignmentIndex, type Id, type ViewKey } from '@/models';
import type ModelService from './ModelService';

export default class AssignmentService implements ModelService<Assignment>  {
  objectViewKeys: ViewKey[] = [
    { key: 'id', kind: 'text' },
    { key: 'name', kind: 'text' },
    { key: 'description', kind: 'code' },
    { key: 'problemExplanation', kind: 'code' },
    { key: 'requiredSectionIds', kind: 'select' },
    { key: 'courseId', kind: 'select' },
  ];

  make(): Assignment {
    return new Assignment({});
  }

  static async createAsync(assignment: Assignment, options: HttpOptions = {}): Promise<Assignment> {
    try {
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
    }
    catch (err: any) {
      options.toast?.error(`Failed to delete assignment: ${err}`);
      throw err;
    }
  }

  static async getAsync(id: Id, options: HttpOptions = {}): Promise<Assignment> {
    try {
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
    }
    catch (err: any) {
      options.toast?.error(`Failed to update assignment: ${err}`);
      throw err;
    }
  }
};
