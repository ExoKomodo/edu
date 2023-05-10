<template>
  <div class="coursePostBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 text-white">
      <p class="text-2xl font-bold border-white rounded border-2 p-1 pl-2">{{ course.name?.toUpperCase() }}</p>
      <p class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2 text-slate-400">{{ course.description }}</p>
      <p class="text-white border-white border-2 rounded p-1 pl-2 my-2" v-html="course.content"></p>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'CoursePost',
  props: {
    name: String,
    description: String,
    content: String,
  },
  data() {
    return {
      course: {
        name: this.name,
        description: this.description,
        content: this.content,
      },
    };
  }
});
</script>
