``<script setup lang="ts">
import type { BlogIndex, BlogMetadata, Id } from '../models';
import BlogService from '@/services/BlogService';
import BlogLink from '../components/BlogLink.vue';

const blogIndex: BlogIndex = await BlogService.getAll();

// NOTE: Needed to fool the type checker with the loop values
function castToBlogId(value: number) {
  return (value as unknown) as Id;
}

// NOTE: Needed to fool the type checker with the loop values
function castToBlogMetadata(value: [string, BlogMetadata]) {
  return (value as unknown) as BlogMetadata;
}
</script>

<template>
  <div class="blogBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-5 my-5">
      <h1 class="p-2 bg-gray-800 text-white rounded flex justify-center text-3xl font-bold my-3">blogs</h1>
        <BlogLink v-for="(blog, id) of blogIndex.blogs"
                  class="p-2 bg-gray-800 text-white rounded flex pl-5 my-3"
                  :id="castToBlogId(id)"
                  :title="castToBlogMetadata(blog).title"
                  :description=" castToBlogMetadata(blog).description" />
    </div>
  </div>
</template>
