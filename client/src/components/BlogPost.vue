<template>
  <div class="bg-slate-400">
    <h1>{{ blog.title }}</h1>
    <h2>{{ blog.description }}</h2>
    <p>{{ blog.content }}</p>
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
