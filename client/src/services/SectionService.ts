import { Section, type ViewKey, type Id, type SectionIndex, SectionMetadata } from '@/models';
import HttpServiceV1, { type HttpOptions } from './HttpServiceV1';

export default class SectionService {
  objectViewKeys: ViewKey[] = [
    { key: 'id', kind: 'text' },
  ];

  make(): Section {
    return new Section({});
  }

  static async createAsync(section: Section, options: HttpOptions = {}): Promise<Section> {
    try {
      return new Section(
        await HttpServiceV1.postAsync('section', section, options));
    }
    catch (err: any) {
      options.toast?.error(`Failed to create section: ${err}`);
      throw err;
    }
  }

  static async deleteAsync(id: Id, options: HttpOptions = {}): Promise<Section> {
    try {
      return new Section(
        await HttpServiceV1.deleteAsync('section', id, options));
    }
    catch (err: any) {
      options.toast?.error(`Failed to delete section: ${err}`);
      throw err;
    }
  }

  static async getAsync(id: Id, options: HttpOptions = {}): Promise<Section> {
    try {
      return new Section(
        await HttpServiceV1.getAsync('section', id, options));
    }
    catch (err: any) {
      options.toast?.error(`Failed to get section: ${err}`);
      throw err;
    }
  }

  static async getAllAsync(options: HttpOptions = {}): Promise<SectionIndex> {
    try {
      const index = await HttpServiceV1.getAllAsync('section', options) as any;
      Object.keys(index).forEach(function(key, _) {
        index[key] = new SectionMetadata(index[key]);
      });
      return new Map<Id, SectionMetadata>(Object.entries(index));
    }
    catch (err: any) {
      options.toast?.error(`Failed to get all sections: ${err}`);
      throw err;
    }
  }

  static async updateAsync(section: Section, options: HttpOptions = {}): Promise<Section> {
    try {
      return new Section(
        await HttpServiceV1.putAsync('section', section, options));
    }
    catch (err: any) {
      options.toast?.error(`Failed to update section: ${err}`);
      throw err;
    }
  }
};
