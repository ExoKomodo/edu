<script setup lang="ts">
import CoursePost from '../components/CoursePost.vue';
import CourseService from '../services/CourseService';
import type { Course } from '../models';

const props = defineProps({
  id: String
});
const course: Course = await CourseService.get(props.id as string);
</script>

<template>
  <CoursePost :name=course.metadata.name :content=course.content :description=course.metadata.description />
</template>
