<template>
  <div>
    <a :href="`/blog/${id}`">{{ title }}: {{ description }}</a>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'BlogLink',
  props: {
    title: String,
    description: String,
    id: String,
  },
});
</script>

<style scoped>
a {
  text-decoration: underline;
}
</style>