<script setup lang="ts">
import BlogService from '@/services/BlogService';
import BlogLink from '../components/BlogLink.vue';
import type { Blog } from '../models';

const blogs: Blog[] = BlogService.getManyStubs(['1', '2', '3']);
</script>

<template>
  <h1>Blogs</h1>
  <BlogLink v-for="blog in blogs"
            :id=blog.id
            :title=blog.title
            :description=blog.description />
</template>
