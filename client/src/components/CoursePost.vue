<template>
  <div class="coursePostBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 text-white">
      <p v-if="auth0.isAuthenticated" class="text-2xl font-bold border-slate-400 rounded border-2 p-1 pl-2">{{ state.name?.toUpperCase() }}</p>
      <p v-if="auth0.isAuthenticated" class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2" v-html="state.description"></p>
      <div v-if="auth0.isAuthenticated" class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2" v-html="props.templatedContent"></div>
      <CourseEditor :handler="saveCourseAsync"
                    handlerText="Update"
                    :courseId="state.id"
                    :courseContent="state.content"
                    :courseDescription="state.description"
                    :courseName="state.name"></CourseEditor>
    </div>
  </div>
</template>

<script setup lang="ts">
import AuthService from '@/services/AuthService';
import CourseEditor, { type CourseEditorState } from '@/components/CourseEditor.vue';
import CourseService from '@/services/CourseService';
import type { Course } from '@/models';
import { reactive } from 'vue';
import { useAuth0 } from '@auth0/auth0-vue';
import { useToast } from 'vue-toastification';

const auth0 = useAuth0();
const toast = useToast();

const props = defineProps<{
  id: string,
  name: string,
  description: string,
  content: string,
  templatedContent: string,
}>();

const state = reactive({
  isEditMode: false,
  showPreview: false,
  id: props.id,
  name: props.name,
  description: props.description,
  content: props.content,
});

async function saveCourseAsync(state: CourseEditorState) {
  const courseToUpdate: Course = {
    id: props.id,
    content: state.content,
    metadata: {
      name: state.name,
      description: state.description,
    },
  };
  await CourseService.updateAsync(courseToUpdate, { toast: toast, token: await AuthService.getAccessTokenAsync(auth0, { toast: toast }) });
  window.location.reload();
}
</script>
