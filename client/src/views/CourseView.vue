<template>
  <div class="courseBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-5 my-5">
      <h1 class="p-2 bg-mysticStone text-white rounded flex justify-center text-3xl font-bold my-3">courses</h1>
      <div v-if="state.isLoading" class="flex place-content-center">
        <Spinner></Spinner>
      </div>
      <div v-else>
        <span v-for="[id, course] of state.courseIndex">
          <CourseLink class="p-2 bg-mysticStone text-white rounded flex pl-5 my-3" :id="id"
            :name="course.name" :description="course.description" />
          <Button v-if="AuthService.isAdmin(auth0)" :handler="async () => await deleteCourseAsync(id)"
            text="Delete?" class="w-20"></Button>
        </span>
        <CourseEditor :handler="createCourseAsync" handlerText="Create" courseId="" courseContent=""
          courseDescription="" courseName=""></CourseEditor>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import AuthService from '@/services/AuthService';
import Button from '@/components/Button.vue';
import CourseLink from '@/components/CourseLink.vue';
import CourseService from '@/services/CourseService';
import Spinner from '@/components/Spinner.vue';
import { onMounted, reactive } from 'vue';
import { useAuth0 } from '@auth0/auth0-vue';
import { useToast } from "vue-toastification";
import CourseEditor from '@/components/CourseEditor.vue';
import type { CourseIndex } from '@/models';


const auth0 = useAuth0();
const toast = useToast();

const state = reactive({
  isLoading: true,
  courseIndex: {} as CourseIndex,
});

async function createCourseAsync(state: any) {
  state.metadata = {
    name: state.name,
    description: state.description,
  };
  const courseToCreate = {
    id: state.id,
    content: state.content,
    metadata: state.metadata,
  };
  console.log(courseToCreate);
  await CourseService.createAsync(
    courseToCreate,
    {
      toast: toast,
      token: await AuthService.getAccessTokenAsync(auth0, { toast: toast }),
    }
  );
  window.location.reload();
}

async function deleteCourseAsync(id: string) {
  await CourseService.deleteAsync(
    id,
    {
      toast: toast,
      token: await AuthService.getAccessTokenAsync(auth0, { toast: toast }),
    }
  );
  window.location.reload();
}

onMounted(async () => {
  try {
    state.courseIndex = await CourseService.getAllAsync(
      {
        toast: toast,
        token: await AuthService.getAccessTokenAsync(auth0, { toast: toast }),
      }
    );
  }
  finally {
    state.isLoading = false;
  }
})
</script>import type { CourseIndex, Id, CourseMetadata } from '@/models';

