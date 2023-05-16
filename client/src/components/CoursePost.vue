<template>
  <div class="coursePostBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 text-white">
      <p v-if="auth0.isAuthenticated" class="text-2xl font-bold border-slate-400 rounded border-2 p-1 pl-2">{{ state.name?.toUpperCase() }}</p>
      <p v-if="auth0.isAuthenticated" class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2">{{ state.description }}</p>
      <div v-if="auth0.isAuthenticated" class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2" v-html="state.content"></div>
      <CourseEditor :handler="saveCourse"
                    handlerText="Update"
                    :courseId="state.id"
                    :courseContent="state.content"
                    :courseDescription="state.description"
                    :courseName="state.name"></CourseEditor>
    </div>
  </div>
</template>

<script setup lang="ts">
import CourseEditor, { type CourseEditorState } from '@/components/CourseEditor.vue';
import CourseService from '../services/CourseService';
import { reactive } from 'vue';
import AuthService from '@/services/AuthService';
import { useAuth0 } from '@auth0/auth0-vue';
import type { Course } from '@/models';

const auth0 = useAuth0();

const course = defineProps<{
  id: string,
  name: string,
  description: string,
  content: string,
}>();

const state = reactive({
  isEditMode: false,
  showPreview: false,
  id: course.id,
  name: course.name,
  description: course.description,
  content: course.content,
});

function saveCourse(state: CourseEditorState) {
  const courseToUpdate: Course = {
    id: course.id,
    content: state.content,
    metadata: {
      name: state.name,
      description: state.description,
    },
  };
  AuthService.getAccessTokenAsync(auth0).then(token => {
    CourseService.update(courseToUpdate, token).then(() => {
      window.location.reload();
    });
  });
}
</script>
