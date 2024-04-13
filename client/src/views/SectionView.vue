<template>
  <div class="sectionBackground min-h-screen">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-5 my-5">
      <h1 class="p-2 bg-mysticStone text-white rounded flex justify-center text-3xl font-bold my-3">Sections</h1>
      <div v-if="state.isLoading" class="flex place-content-center">
        <Spinner></Spinner>
      </div>
      <div v-else>
        <span v-for="(section, id) of state.sectionIndex">
          <SectionLink class="p-2 bg-mysticStone text-white rounded flex pl-5 my-3" :id="castToSectionId(id)"
            :courseId=props.courseId :name="castToSectionMetadata(section).name"
            :description="castToSectionMetadata(section).description" />
          <Button v-if="AuthService.isAdmin(auth0)" :handler="async () => await deleteSectionAsync(castToSectionId(id))"
            text="Delete?" class="w-20"></Button>
        </span>
        <SectionEditor :handler="createSectionAsync" handlerText="Create" sectionId="" sectionContent=""
          :sectionDifficulty=1 sectionDescription="" sectionName=""></SectionEditor>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import AuthService from '@/services/AuthService';
import Button from '@/components/Button.vue';
import SectionEditor, { type SectionEditorState } from '@/components/SectionEditor.vue';
import SectionLink from '@/components/SectionLink.vue';
import SectionService from '@/services/SectionService';
import Spinner from '@/components/Spinner.vue';
import type { SectionIndex, SectionMetadata, Id } from '@/models';
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
  sectionIndex: {} as SectionIndex,
});

async function createSectionAsync(state: SectionEditorState) {
  const sectionToCreate = {
    id: state.id,
    content: state.content,
    difficulty: Number.parseInt(state.difficulty) as number,
    metadata: {
      name: state.name,
      description: state.description,
      courseId: props.courseId,
    },
  };
  await SectionService.createAsync(
    sectionToCreate,
    {
      toast: toast,
      token: await AuthService.getAccessTokenAsync(auth0, { toast: toast }),
    }
  );
  window.location.reload();
}

async function deleteSectionAsync(id: string) {
  await SectionService.deleteAsync(
    id,
    {
      toast: toast,
      token: await AuthService.getAccessTokenAsync(auth0, { toast: toast }),
    }
  );
  window.location.reload();
}

// NOTE: Needed to fool the type checker with the loop values
function castToSectionId(value: number) {
  return (value as unknown) as Id;
}

// NOTE: Needed to fool the type checker with the loop values
function castToSectionMetadata(value: [string, SectionMetadata]) {
  return (value as unknown) as SectionMetadata;
}

onMounted(async () => {
  try {
    state.sectionIndex = await SectionService.getAllAsync(
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
