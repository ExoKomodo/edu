<template>
  <div v-if="AuthService.isAdmin(auth0)">
    <input type="checkbox"
           id="edit-mode"
           name="editMode"
           v-model="state.isEditMode">
    <label for="edit-mode"> Edit mode?</label>
    <span class="inline-block"
          :class="{ invisible: !state.isEditMode }">
      <Button :handler="() => handler(state)"
              :text="handlerText"></Button>
    </span>
    <div :class="{ invisible: !state.isEditMode }">
      <input type="checkbox"
             id="show-preview"
             name="showPreview"
             v-model="state.showPreview">
      <label for="show-preview"> Show preview?</label>
    </div>
  </div>
  <div v-if="AuthService.isAdmin(auth0) && (state.isEditMode ? state.showPreview : true)">
    <p class="text-2xl font-bold border-white rounded border-2 p-1 pl-2">{{ state.name?.toUpperCase() }}</p>
    <p class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2 text-slate-400">{{ state.description }}</p>
    <p class="text-white border-white border-2 rounded p-1 pl-2 my-2"
       v-html="state.content"></p>
  </div>
  <div v-if="AuthService.isAdmin(auth0) && state.isEditMode">
    <div class="text-white border-white border-2 rounded p-1 pl-2 my-2 mr-12">Name</div>
    <CodeEditor v-model="state.name"
                :height="2"></CodeEditor>
    <div class="text-white border-white border-2 rounded p-1 pl-2 p my-2 mr-1">Description</div>
    <CodeEditor v-model="state.description"
                :height="2"></CodeEditor>
    <div class="text-white border-white border-2 rounded p-1 pl-2 my-2 mr-8"
         language="html">Content</div>
    <CodeEditor v-model="state.content"
                :height="40"></CodeEditor>
  </div>
</template>

<script setup lang="ts">
import Button from '@/components/Button.vue';
import CodeEditor from '@/components/CodeEditor.vue';
import { reactive } from 'vue';
import AuthService from '@/services/AuthService';
import { useAuth0 } from '@auth0/auth0-vue';

export type CourseEditorState = {
  isEditMode: boolean,
  showPreview: boolean,
  id: string,
  name: string,
  description: string,
  content: string,
};

const auth0 = useAuth0();

const props = defineProps<{
  handler: (state: CourseEditorState) => void,
  handlerText: string,
  courseId: string,
  courseName: string,
  courseDescription: string,
  courseContent: string,
}>()

const state = reactive({
  isEditMode: false,
  showPreview: false,
  id: props.courseId,
  name: props.courseName,
  description: props.courseDescription,
  content: props.courseContent,
});
</script>

<style scoped>
a {
  text-decoration: underline;
}
</style>