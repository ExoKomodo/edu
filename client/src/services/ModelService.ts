import type { ViewKey } from "@/models";
import BlobService from "./BlobService";
import type { HttpOptions } from "./HttpServiceV1";
import SectionService from "./SectionService";
import CourseService from "./CourseService";
import AssignmentService from "./AssignmentService";
import BlogService from "./BlogService";

abstract class ModelService<T> {
  abstract objectViewKeys: ViewKey[];

  static inject(kind: string) {
    switch (kind) {
      case 'assignment':
        return new AssignmentService();
      case 'blog':
        return new BlogService();
      case 'course':
        return new CourseService();
      case 'section':
        return new SectionService();
      default:
        throw new Error(`Unknown kind: ${kind}`);
    }
  }

  abstract make(): T;

  static async fillTemplateAsync(template: string, options: HttpOptions = {}): Promise<string> {
    if (!template) {
      return '';
    }
    // NOTE: Match and captures what is between ${}, to replace with presigned URLs
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
}

export default ModelService;
