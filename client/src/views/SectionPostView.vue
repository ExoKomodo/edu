<template>
  <div v-if="state.isLoading" class="flex place-content-center">
    <Spinner></Spinner>
  </div>
  <div v-else>
    <SectionPost :id=id
                :name=state.section.metadata.name
                :content=state.section.content
                :difficulty=state.section.difficulty
                :description=state.section.metadata.description
                :course-id=props.courseId />
  </div>
</template>

<script setup lang="ts">
import AuthService from '@/services/AuthService';
import SectionPost from '@/components/SectionPost.vue';
import SectionService from '@/services/SectionService';
import Spinner from '@/components/Spinner.vue';
import type { Section } from '@/models';
import { onMounted, reactive } from 'vue';
import { useAuth0 } from '@auth0/auth0-vue';
import { useToast } from 'vue-toastification';

const auth0 = useAuth0();
const props = defineProps<{
  id: string,
  courseId: string,
}>();
const state = reactive({
  isLoading: true,
  section: {} as Section,
});
const toast = useToast();

onMounted(async () => {
  try {
    state.section = await SectionService.getAsync(props.id, {
      toast: toast,
      token: await AuthService.getAccessTokenAsync(auth0),
    });
  }
  finally {
    state.isLoading = false;
  }
});
</script>
