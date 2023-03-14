<template>
  <div>
    <h1>{{ blog.title }}</h1>
    <h2>{{ blog.description }}</h2>
    <p>{{ blog.content ?? "Missing blog content..." }}</p>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import BlogService from '../services/BlogService';

export default defineComponent({
  name: 'BlogPost',
  props: {
    id: String,
  },
  async asyncData() {
    return {
      blog: await BlogService.get(this.id as string),
    };
  }
});
</script>
