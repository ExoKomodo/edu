<template>
  <div class="courseBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-5 my-5">
      <h1 class="p-2 bg-mysticStone text-white rounded flex justify-center text-3xl font-bold my-3">courses</h1>
      <div v-if="state.isLoading" class="flex place-content-center">
        <Spinner></Spinner>
      </div>
      <div v-else>
        <span v-for="(course, id) of state.courseIndex">
          <CourseLink
                    class="p-2 bg-mysticStone text-white rounded flex pl-5 my-3"
                    :id="castToCourseId(id)"
                    :name="castToCourseMetadata(course).name"
                    :description=" castToCourseMetadata(course).description" />
          <Button v-if="AuthService.isAdmin(auth0)" :handler="() => deleteCourse(castToCourseId(id))" text="Delete?" class="w-20"></Button>
        </span>
        <CourseEditor :handler="createCourse"
                    handlerText="Create"
                    courseId=""
                    courseContent=""
                    courseDescription=""
                    courseName=""></CourseEditor>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import AuthService from '@/services/AuthService';
import Button from '@/components/Button.vue';
import CourseEditor, { type CourseEditorState } from '@/components/CourseEditor.vue';
import CourseLink from '@/components/CourseLink.vue';
import CourseService from '@/services/CourseService';
import Spinner from '@/components/Spinner.vue';
import type { CourseIndex, CourseMetadata, Id } from '@/models';
import { onMounted, reactive } from 'vue';
import { useAuth0 } from '@auth0/auth0-vue';

const auth0 = useAuth0();

const state = reactive({
  isLoading: true,
  courseIndex: {} as CourseIndex,
});

function createCourse(state: CourseEditorState) {
  const courseToCreate = {
    id: state.id,
    content: state.content,
    metadata: {
      name: state.name,
      description: state.description,
    },
  };
  AuthService.getAccessTokenAsync(auth0).then(token => {
    CourseService.create(courseToCreate, token).then(() => {
      window.location.reload();
    });
  });
}

function deleteCourse(id: string) {
  AuthService.getAccessTokenAsync(auth0).then(token => {
    CourseService.delete(id, token).then(() => {
      window.location.reload();
    });
  });
}

// NOTE: Needed to fool the type checker with the loop values
function castToCourseId(value: number) {
  return (value as unknown) as Id;
}

// NOTE: Needed to fool the type checker with the loop values
function castToCourseMetadata(value: [string, CourseMetadata]) {
  return (value as unknown) as CourseMetadata;
}

onMounted(async () => {
  state.courseIndex = await CourseService.getAll(
    await AuthService.getAccessTokenAsync(auth0)
  );
  state.isLoading = false;
})
</script>
