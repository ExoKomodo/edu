<template>
  <div class="coursePostBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 text-white">
      <div v-if="AuthService.isAdmin(auth0)">
        <input type="checkbox" id="edit-mode" name="editMode" v-model="state.isEditMode">
        <label for="edit-mode"> Edit mode?</label>
        <span class="inline-block" :class="{ invisible: !state.isEditMode }">
          <Button :handler="saveCourse" text="Save"></Button>
        </span>
      </div>
      <div v-if="!state.isEditMode && AuthService.isAdmin(auth0)">
        <p class="text-2xl font-bold border-white rounded border-2 p-1 pl-2">{{ state.name?.toUpperCase() }}</p>
        <p class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2 text-slate-400">{{ state.description }}</p>
        <p class="text-white border-white border-2 rounded p-1 pl-2 my-2" v-html="state.content"></p>
      </div>
      <div v-if="state.isEditMode && AuthService.isAdmin(auth0)">
        <div class="text-white border-white border-2 rounded p-1 pl-2 my-2 mr-12">Name</div>
        <CodeEditor v-model="state.name" :height="2"></CodeEditor>
        <div class="text-white border-white border-2 rounded p-1 pl-2 p my-2 mr-1">Description</div>
        <CodeEditor v-model="state.description" :height="2"></CodeEditor>
        <div class="text-white border-white border-2 rounded p-1 pl-2 my-2 mr-8">Content</div>
        <CodeEditor v-model="state.content" :height="40"></CodeEditor>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import CodeEditor from '@/components/CodeEditor.vue';
import Button from '@/components/Button.vue';
import CourseService from '../services/CourseService';
import { reactive } from 'vue';
import AuthService from '@/services/AuthService';
import { useAuth0 } from '@auth0/auth0-vue';

const auth0 = useAuth0();

const course = defineProps<{
  id: string,
  name: string,
  description: string,
  content: string,
}>();

const state = reactive({
  isEditMode: false,
  name: course.name,
  description: course.description,
  content: course.content,
});

function saveCourse() {
  const courseToUpdate = {
    id: course.id,
    content: state.content,
    metadata: {
      name: state.name,
      description: state.description,
    },
  };
  AuthService.getAccessTokenAsync(auth0).then(token => {
    CourseService.update(courseToUpdate, token);
  })
}
</script>
