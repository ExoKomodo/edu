<template>
  <div class="assignmentPostBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 text-white">
      <p v-if="auth0.isAuthenticated" class="text-2xl font-bold border-slate-400 rounded border-2 p-1 pl-2">{{ state.name?.toUpperCase() }}</p>
      <div v-if="auth0.isAuthenticated" class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2">
        <figure>
          <figcaption>Required Sections</figcaption>
          <ul class="list-disc pl-6">
            <li v-for="sectionId in state.sectionIds">
              {{ sectionId }}
            </li>
          </ul>
        </figure>
      </div>
      <p v-if="auth0.isAuthenticated" class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2">{{ state.description }}</p>
      <p v-if="auth0.isAuthenticated" class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2" v-html="state.problemExplanation"></p>
      <AssignmentEditor :handler="saveAssignmentAsync"
                    handlerText="Update"
                    :assignmentId="state.id"
                    :assignmentProblemExplanation="state.problemExplanation"
                    :assignmentDescription="state.description"
                    :assignmentName="state.name"
                    :assignmentSectionIds="state.sectionIds"></AssignmentEditor>
    </div>
  </div>
</template>

<script setup lang="ts">
import AuthService from '@/services/AuthService';
import AssignmentEditor, { type AssignmentEditorState } from '@/components/AssignmentEditor.vue';
import AssignmentService from '@/services/AssignmentService';
import type { Assignment, Id } from '@/models';
import { reactive } from 'vue';
import { useAuth0 } from '@auth0/auth0-vue';
import { useToast } from 'vue-toastification';

const auth0 = useAuth0();
const toast = useToast();

const props = defineProps<{
  id: string,
  courseId: string,
  name: string,
  description: string,
  problemExplanation: string,
  sectionIds: Id[],
}>();

const state = reactive({
  isEditMode: false,
  showPreview: false,
  id: props.id,
  name: props.name,
  description: props.description,
  problemExplanation: props.problemExplanation,
  sectionIds: props.sectionIds,
});

async function saveAssignmentAsync(state: AssignmentEditorState) {
  const assignmentToUpdate: Assignment = {
    id: props.id,
    problemExplanation: state.problemExplanation,
    metadata: {
      name: state.name,
      description: state.description,
      requiredSectionIds: state.requiredSectionIds,
      courseId: props.courseId,
    },
  };
  await AssignmentService.updateAsync(assignmentToUpdate, { toast: toast, token: await AuthService.getAccessTokenAsync(auth0, { toast: toast }) });
  window.location.reload();
}
</script>
