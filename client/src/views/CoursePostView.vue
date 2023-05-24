<template>
  <div v-if="state.isLoading" class="flex place-content-center">
    <Spinner></Spinner>
  </div>
  <div v-else>
    <CoursePost :id="id"
                :name="state.course.metadata.name"
                :content="state.course.content"
                :templated-content="state.course.templatedContent ? state.course.templatedContent : ''"
                :description=state.course.metadata.description />
  </div>
</template>

<script setup lang="ts">
import AuthService from '@/services/AuthService';
import CoursePost from '@/components/CoursePost.vue';
import CourseService from '@/services/CourseService';
import Spinner from '@/components/Spinner.vue';
import type { Course } from '@/models';
import { onMounted, reactive } from 'vue';
import { useAuth0 } from '@auth0/auth0-vue';
import { useToast } from 'vue-toastification';

const auth0 = useAuth0();
const props = defineProps<{
  id: string,
}>();
const state = reactive({
  isLoading: true,
  course: {} as Course,
});
const toast = useToast();

onMounted(async () => {
  try {
    state.course = await CourseService.getAsync(props.id, {
      toast: toast,
      token: await AuthService.getAccessTokenAsync(auth0),
    });
  }
  finally {
    state.isLoading = false;
  }
});
</script>
