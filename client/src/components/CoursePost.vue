<template>
  <div class="coursePostBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 text-white">
      <div>
        <input type="checkbox" id="edit-mode" name="editMode" v-model="state.isEditMode">
        <label for="edit-mode"> Edit mode?</label><br>
      </div>
      <div v-if="!state.isEditMode">
        <p class="text-2xl font-bold border-white rounded border-2 p-1 pl-2">{{ state.name?.toUpperCase() }}</p>
        <p class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2 text-slate-400">{{ state.description }}</p>
        <!-- TODO: Only show editor in edit mode -->
        <p class="text-white border-white border-2 rounded p-1 pl-2 my-2" v-html="state.content"></p>
      </div>
      <div v-if="state.isEditMode">
        <span class="text-white border-white border-2 rounded p-1 pl-2 my-2 mr-12">Edit name</span>
        <CodeEditor v-model="state.name" :height="3"></CodeEditor>
        <span class="text-white border-white border-2 rounded p-1 pl-2 p my-2 mr-1">Edit description</span>
        <CodeEditor v-model="state.description" :height="3"></CodeEditor>
        <span class="text-white border-white border-2 rounded p-1 pl-2 my-2 mr-8">Edit content</span>
        <CodeEditor v-model="state.content" :height="40"></CodeEditor>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import CodeEditor from '@/components/CodeEditor.vue';
import { reactive } from 'vue';

const course = defineProps({
  name: String,
  description: String,
  content: String,
});

let isEditMode = false;
const state = reactive({
  isEditMode: false,
  name: course.name,
  description: course.description,
  content: course.content,
});

setInterval(() => console.log(state.isEditMode), 2000);
</script>
