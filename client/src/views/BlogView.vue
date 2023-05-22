``<script setup lang="ts">
import BlogLink from '@/components/BlogLink.vue';
import BlogService from '@/services/BlogService';
import Spinner from '@/components/Spinner.vue';
import type { BlogIndex, BlogMetadata, Id } from '@/models';
import { onMounted, reactive } from 'vue';
import { useToast } from 'vue-toastification';

const state = reactive({
  isLoading: true,
  blogIndex: {} as BlogIndex,
});
const toast = useToast();

// NOTE: Needed to fool the type checker with the loop values
function castToBlogId(value: number) {
  return (value as unknown) as Id;
}

// NOTE: Needed to fool the type checker with the loop values
function castToBlogMetadata(value: [string, BlogMetadata]) {
  return (value as unknown) as BlogMetadata;
}

onMounted(async () => {
  try {
    state.blogIndex = await BlogService.getAllAsync({ toast: toast });
  }
  finally {
    state.isLoading = false;
  }
});
</script>

<template>
  <div class="blogBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-5 my-5">
      <h1 class="p-2 bg-mysticStone text-white rounded flex justify-center text-3xl font-bold my-3">blogs</h1>
      <div v-if="state.isLoading" class="flex place-content-center">
        <Spinner></Spinner>
      </div>
      <div v-else>
        <BlogLink v-for="(blog, id) of state.blogIndex.blogs"
                  class="p-2 bg-mysticStone text-white rounded flex pl-5 my-3"
                  :id="castToBlogId(id)"
                  :title="castToBlogMetadata(blog).title"
                  :description=" castToBlogMetadata(blog).description" />
      </div>
    </div>
  </div>
</template>
