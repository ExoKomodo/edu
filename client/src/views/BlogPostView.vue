<script setup lang="ts">
import BlogPost from '../components/BlogPost.vue';
import BlogService from '../services/BlogService';

const props = defineProps({
  id: String
});
const blog = await BlogService.get(props.id as string);
</script>

<template>
  <BlogPost :title=blog.title :content=blog.content :description=blog.description />
</template>
