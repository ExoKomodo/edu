<template>
  <div>
    <p>{{ blog?.content }}</p>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import BlogService from '../services/BlogService';

export default defineComponent({
  name: 'BlogPost',
  props: {
    id: String,
  },
  data() {
    return {
      blog: BlogService.get(this.id as string),
    };
  }
});
</script>
