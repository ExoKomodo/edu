<template>
  <div v-if="AuthService.isAdmin(auth0)"
    class="bg-mysticStone text-white border-slate-400 border-2 rounded p-1 pl-2 my-2">
    <div class="text-2xl">Model Editor</div>
    <input type="checkbox" id="edit-mode" name="editMode" v-model="state.isEditMode">
    <label for="edit-mode"> {{ handlerText }} mode?</label>
    <span class="inline-block" :class="{ invisible: !state.isEditMode }">
      <Button :handler="() => handler(state.viewModel)" :text="handlerText"></Button>
    </span>
    <div :class="{ invisible: !state.isEditMode }">
      <input type="checkbox" id="show-preview" name="showPreview"
        v-model="state.showPreview">
      <label for="show-preview"> Show preview?</label>
    </div>
    <div v-if="AuthService.isAdmin(auth0) && state.isEditMode">
      <div v-for="viewKey in modelService.objectViewKeys">
        <div class="capitalize text-white border-white border-2 rounded p-1 pl-2 my-2 mr-12">{{ viewKey.key }}</div>
        <input v-if="viewKey.kind === 'text'" v-model="(state.viewModel as any)[viewKey.key]" type="text">
        <input v-if="viewKey.kind === 'number'" v-model="(state.viewModel as any)[viewKey.key]" type="number">
        <input v-if="viewKey.kind === 'select'" v-model="(state.viewModel as any)[viewKey.key]" type="text" placeholder="TODO: NOT IMPLEMENTED">
        <CodeEditor v-if="viewKey.kind === 'code'" v-model="(state.viewModel as any)[viewKey.key]" :height="viewKey.height ?? 2"></CodeEditor>
      </div>

      <!-- TODO: Enable preview by embedding the View component for the type -->
    </div>
  </div>
</template>

<script setup lang="ts">
import Button from '@/components/Button.vue';
import CodeEditor from '@/components/CodeEditor.vue';
import { reactive, type UnwrapRef } from 'vue';
import AuthService from '@/services/AuthService';
import { useAuth0 } from '@auth0/auth0-vue';
import { useToast } from 'vue-toastification';
import ModelService from '@/services/ModelService';
import type { ViewModel } from '@/models';

const auth0 = useAuth0();
const toast = useToast();

const props = defineProps<{
  handler: (viewModel: UnwrapRef<ViewModel>) => void,
  handlerText: string,
  modelKind: string
}>();

type EditorState = {
  isEditMode: boolean,
  showPreview: boolean,
  viewModel: ViewModel,
};

const modelService = ModelService.inject(props.modelKind) as ModelService<ViewModel>;

const state = reactive<EditorState>({
  isEditMode: false,
  showPreview: false,  
  viewModel: modelService.make()
});

const token = await AuthService.getAccessTokenAsync(auth0, { toast: toast });
</script>
