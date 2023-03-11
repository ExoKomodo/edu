<script setup lang="ts">
import BlogPost from '../components/BlogPost.vue';

const props = defineProps({
  id: String
});
</script>

<template>
  <BlogPost :id=id />
</template>
