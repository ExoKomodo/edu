import type { Ref } from "vue";

export default class Helpers {
  static getEditor(codeEditor: Ref<any>) {
    return (codeEditor.value && codeEditor.value._editor) ? codeEditor.value._editor : (codeEditor as any)._editor;
  }

  static changeEditorContent(codeEditor: Ref<any>, content: string) {
    Helpers.getEditor(codeEditor).getSession().setValue(content);
  }
}