<template>
  <div class="assignmentBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-5 my-5">
      <h1 class="p-2 bg-mysticStone text-white rounded flex justify-center text-3xl font-bold my-3">Assignments</h1>
      <div v-if="state.isLoading" class="flex place-content-center">
        <Spinner></Spinner>
      </div>
      <div v-else>
        <span v-for="(assignment, id) of state.assignmentIndex">
          <AssignmentLink
                    class="p-2 bg-mysticStone text-white rounded flex pl-5 my-3"
                    :id="castToAssignmentId(id)"
                    :courseId=props.courseId
                    :name="castToAssignmentMetadata(assignment).name"
                    :description=" castToAssignmentMetadata(assignment).description" />
          <Button v-if="AuthService.isAdmin(auth0)" :handler="async () => await deleteAssignmentAsync(castToAssignmentId(id))" text="Delete?" class="w-20"></Button>
        </span>
        <AssignmentEditor :handler="createAssignmentAsync"
                    handlerText="Create"
                    assignmentId=""
                    assignmentProblemExplanation=""
                    assignmentDescription=""
                    assignmentName=""
                    :assignmentSectionIds="[]"></AssignmentEditor>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import AuthService from '@/services/AuthService';
import Button from '@/components/Button.vue';
import AssignmentEditor, { type AssignmentEditorState } from '@/components/AssignmentEditor.vue';
import AssignmentLink from '@/components/AssignmentLink.vue';
import AssignmentService from '@/services/AssignmentService';
import Spinner from '@/components/Spinner.vue';
import type { AssignmentIndex, AssignmentMetadata, Id } from '@/models';
import { onMounted, reactive } from 'vue';
import { useAuth0 } from '@auth0/auth0-vue';
import { useToast } from "vue-toastification";

const auth0 = useAuth0();
const toast = useToast();

const props = defineProps<{
  courseId: string,
}>();
const state = reactive({
  isLoading: true,
  assignmentIndex: {} as AssignmentIndex,
});

async function createAssignmentAsync(state: AssignmentEditorState) {
  const assignmentToCreate = {
    id: state.id,
    problemExplanation: state.problemExplanation,
    metadata: {
      name: state.name,
      description: state.description,
      requiredSectionIds: state.requiredSectionIds,
      courseId: props.courseId,
    },
  };
  await AssignmentService.createAsync(
    assignmentToCreate,
    {
      toast: toast,
      token: await AuthService.getAccessTokenAsync(auth0, { toast: toast }),
    }
  );
  window.location.reload();
}

async function deleteAssignmentAsync(id: string) {
  await AssignmentService.deleteAsync(
    id,
    {
      toast: toast, 
      token: await AuthService.getAccessTokenAsync(auth0, { toast: toast }) ,
    }
  );
  window.location.reload();
}

// NOTE: Needed to fool the type checker with the loop values
function castToAssignmentId(value: number) {
  return (value as unknown) as Id;
}

// NOTE: Needed to fool the type checker with the loop values
function castToAssignmentMetadata(value: [string, AssignmentMetadata]) {
  return (value as unknown) as AssignmentMetadata;
}

onMounted(async () => {
  try {
    state.assignmentIndex = await AssignmentService.getAllAsync(
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
</script>
