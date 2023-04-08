<script setup lang="ts">
import BlogPost from '../components/BlogPost.vue';
import BlogService from '../services/BlogService';

const props = defineProps({
  id: String
});
const blog = await BlogService.get(props.id as string);
const dummyBlog = {
  title: "first blog, for dummies",
  id: "1",
  content: "dummy thicc content",
  description: "bing bang bong ya ya a;lwisdhfka;slkdfhjnas;lkdfhjas;lkdfhj"
}
</script>

<template>
  <BlogPost :title=dummyBlog.title :content=dummyBlog.content :description=dummyBlog.description />
</template>
