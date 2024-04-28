<template>
  <div class="blogBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-5 my-5">
      <h1 class="p-2 bg-mysticStone text-white rounded flex justify-center text-3xl font-bold my-3">blogs</h1>
      <div v-if="state.isLoading" class="flex place-content-center">
        <Spinner></Spinner>
      </div>
      <div v-else>
        <BlogLink v-for="[id, blog] of state.blogIndex.blogs"
          class="p-2 bg-mysticStone text-white rounded flex pl-5 my-3" :id="id"
          :title="blog.title" :description="blog.description" />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
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

onMounted(async () => {
  try {
    state.blogIndex = await BlogService.getAllAsync({ toast: toast });
    console.log(state.blogIndex);
  }
  finally {
    state.isLoading = false;
  }
});
</script>
