<script setup lang="ts">
import BlogPost from '@/components/BlogPost.vue';
import Spinner from '@/components/Spinner.vue';
import BlogService from '@/services/BlogService';
import type { Blog } from '@/models';
import { onMounted, reactive } from 'vue';
import { useToast } from 'vue-toastification';

const props = defineProps<{
  id: string,
}>();
const state = reactive({
  isLoading: true,
  blog: {} as Blog,
});
const toast = useToast();

onMounted(async () => {
  try {
    state.blog = await BlogService.getAsync(props.id, { toast: toast });
  }
  finally {
    state.isLoading = false;
  }
});
</script>

<template>
  <div v-if="state.isLoading" class="flex place-content-center">
    <Spinner></Spinner>
  </div>
  <div v-else>
    <BlogPost :title=state.blog.metadata.title :content=state.blog.content :description=state.blog.metadata.description />
  </div>
  
</template>
