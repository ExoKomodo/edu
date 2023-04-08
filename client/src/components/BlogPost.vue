<template>
  <div class="postBackground h-screen ">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 text-white">
      <p class="text-2xl font-bold border-white rounded border-2 p-1 pl-2">{{ blog.title?.toUpperCase() }}</p>
      <p class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2 text-slate-400">{{ blog.description }}</p>
      <p class="text-white border-white border-2 rounded p-1 pl-2 my-2">{{ blog.content }}</p>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'BlogPost',
  props: {
    title: String,
    description: String,
    content: String,
  },
  data() {
    return {
      blog: {
        title: this.title,
        description: this.description,
        content: this.content,
      },
    };
  }
});
</script>
