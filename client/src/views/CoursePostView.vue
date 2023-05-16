<template>
  <CoursePost :id="id" :name=course.metadata.name :content=course.content :description=course.metadata.description />
</template>

<script setup lang="ts">
import CoursePost from '../components/CoursePost.vue';
import CourseService from '../services/CourseService';
import type { Course } from '../models';
import { useAuth0 } from '@auth0/auth0-vue';
import AuthService from '../services/AuthService';

const auth0 = useAuth0();
const props = defineProps<{
  id: string,
}>();
const course: Course = await CourseService.get(props.id, await AuthService.getAccessTokenAsync(auth0));
console.log(course);
</script>
