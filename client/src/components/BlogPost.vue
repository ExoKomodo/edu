<template>
  <div class="postBackground h-screen ">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 text-white">
      <h1>{{ blog.title }}</h1>
      <h2>{{ blog.description }}</h2>
      <p>{{ blog.content }}</p>
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
