<template>
  <div v-if="state.isLoading" class="flex place-content-center">
    <Spinner></Spinner>
  </div>
  <div v-else>
    <AssignmentPost :id=id :name=state.assignment.metadata.name :problemExplanation=state.assignment.problemExplanation
      :description=state.assignment.metadata.description :courseId=props.courseId
      :sectionIds=state.assignment.metadata.requiredSectionIds />
  </div>
</template>

<script setup lang="ts">
import AuthService from '@/services/AuthService';
import AssignmentPost from '@/components/AssignmentPost.vue';
import AssignmentService from '@/services/AssignmentService';
import Spinner from '@/components/Spinner.vue';
import type { Assignment } from '@/models';
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
  assignment: {} as Assignment,
});
const toast = useToast();

onMounted(async () => {
  try {
    state.assignment = await AssignmentService.getAsync(props.id, {
      toast: toast,
      token: await AuthService.getAccessTokenAsync(auth0),
    });
  }
  finally {
    state.isLoading = false;
  }
});
</script>
