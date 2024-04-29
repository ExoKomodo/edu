<template>
  <RouterLink :to="`/course/${id}`" class="hover:bg-midnightGreen flex flex-col transition duration-250">
    <p class="text-2xl" v-html="name"></p>
    <p class="text-gray-400" v-html="description"></p>
  </RouterLink>

</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'CourseLink',
  props: {
    name: {
      type: String,
      required: true,
    },
    description: {
      type: String,
      required: true,
    },
    id: {
      type: String,
      required: true,
    },
  },
});
</script>

<style scoped>
a {
  text-decoration: underline;
}
</style>
