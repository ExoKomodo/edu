``<script setup lang="ts">
import type { CourseIndex, CourseMetadata, Id } from '../models';
import CourseService from '@/services/CourseService';
import CourseLink from '../components/CourseLink.vue';
import { useAuth0 } from '@auth0/auth0-vue';
import AuthService from '../services/AuthService';

const auth0 = useAuth0();

const courseIndex: CourseIndex = await CourseService.getAll(
  await AuthService.getAccessTokenAsync(auth0) 
);

// NOTE: Needed to fool the type checker with the loop values
function castToCourseId(value: number) {
  return (value as unknown) as Id;
}

// NOTE: Needed to fool the type checker with the loop values
function castToCourseMetadata(value: [string, CourseMetadata]) {
  return (value as unknown) as CourseMetadata;
}
</script>

<template>
  <div class="courseBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-5 my-5">
      <h1 class="p-2 bg-mysticStone text-white rounded flex justify-center text-3xl font-bold my-3">courses</h1>
        <CourseLink v-for="(course, id) of courseIndex"
                  class="p-2 bg-mysticStone text-white rounded flex pl-5 my-3"
                  :id="castToCourseId(id)"
                  :name="castToCourseMetadata(course).name"
                  :description=" castToCourseMetadata(course).description" />
    </div>
  </div>
</template>
