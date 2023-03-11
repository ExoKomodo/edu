<script setup lang="ts">
import BlogPost from '../components/BlogPost.vue';
</script>

<template>
  <BlogPost :id="`${$route.params.id as string}`" />
</template>
