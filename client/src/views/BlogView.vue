<script setup lang="ts">
import BlogService from '@/services/BlogService';
import BlogLink from '../components/BlogLink.vue';
import type { BlogIndex } from '../models';

const blogIndex: BlogIndex = await BlogService.getAll();
</script>

<template>
  <div class="blogBackground h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-5 my-5">
      <h1 class="p-2 bg-gray-800 text-white rounded flex justify-center text-3xl font-bold my-3">blogs</h1>
        <BlogLink
                  class="p-2 bg-gray-800 text-white rounded flex pl-5 my-3"
                  v-for="(blog, id) in blogIndex.blogs"
                  :id=id
                  :title=blog.title
                  :description=blog.description.slice(0,150) />
    </div>
  </div>
</template>
