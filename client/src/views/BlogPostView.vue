<script setup lang="ts">
import BlogPost from '../components/BlogPost.vue';
import BlogService from '../services/BlogService';
import type { Blog } from '../models';

const props = defineProps({
  id: {
    type: String,
    required: true,
  }
});
const blog: Blog = await BlogService.get(props.id);
</script>

<template>
  <BlogPost :title=blog.metadata.title :content=blog.content :description=blog.metadata.description />
</template>
