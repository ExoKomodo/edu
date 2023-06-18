<template>
  <div class="sectionPostBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 text-white">
      <p v-if="auth0.isAuthenticated" class="text-2xl font-bold border-slate-400 rounded border-2 p-1 pl-2">{{ state.name?.toUpperCase() }}</p>
      <p v-if="auth0.isAuthenticated" class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2">{{ state.description }}</p>
      <p v-if="auth0.isAuthenticated" class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2">Difficulty: {{ state.difficulty }}</p>
      <p v-if="auth0.isAuthenticated" class="text-xl border-slate-400 rounded border-2 p-1 pl-2 my-2" v-html="state.content"></p>
      <SectionEditor :handler="saveSectionAsync"
                    handlerText="Update"
                    :sectionId="state.id"
                    :sectionContent="state.content"
                    :sectionDifficulty="state.difficulty"
                    :sectionDescription="state.description"
                    :sectionName="state.name"></SectionEditor>
    </div>
  </div>
</template>

<script setup lang="ts">
import AuthService from '@/services/AuthService';
import SectionEditor, { type SectionEditorState } from '@/components/SectionEditor.vue';
import SectionService from '@/services/SectionService';
import type { Section } from '@/models';
import { reactive } from 'vue';
import { useAuth0 } from '@auth0/auth0-vue';
import { useToast } from 'vue-toastification';

const auth0 = useAuth0();
const toast = useToast();

const props = defineProps<{
  id: string,
  courseId: string,
  name: string,
  content: string,
  description: string,
  difficulty: number,
}>();

const state = reactive({
  isEditMode: false,
  showPreview: false,
  id: props.id,
  name: props.name,
  content: props.content,
  description: props.description,
  difficulty: props.difficulty,
});

async function saveSectionAsync(state: SectionEditorState) {
  const sectionToUpdate: Section = {
    id: props.id,
    content: state.content,
    difficulty: Number.parseInt(state.difficulty) as number,
    metadata: {
      name: state.name,
      description: state.description,
      courseId: props.courseId,
    },
  };
  await SectionService.updateAsync(sectionToUpdate, { toast: toast, token: await AuthService.getAccessTokenAsync(auth0, { toast: toast }) });
  window.location.reload();
}
</script>
