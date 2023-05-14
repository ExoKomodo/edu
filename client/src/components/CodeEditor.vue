<template>
    <label>Language:
      <select v-model="selected.language">
        <option v-for="option in languageOptions" v-bind:value="option.id" :selected="selected.language == option.id">{{option.value}}</option>
      </select>
    </label>
    <label>Theme:
      <select v-model="selected.theme">
        <option v-for="option in themeOptions" :value="option.id" :selected="selected.theme == option.id">{{option.value}}</option>
      </select>
    </label>
    <v-ace-editor
      ref="codeEditor"
      :value="modelValue"
      @change="updateContent"
      :lang="selected.language"
      :theme="selected.theme"
      style="height: 20rem"
      :options="{
        useWorker: true,
        enableBasicAutocompletion: true,
        enableSnippets: true,
        enableLiveAutocompletion: true,
      }"/>
</template>

<script setup lang="ts">
import { reactive, ref, type Ref } from 'vue';

// NOTE: Combination of props and emit helps set up `v-model`: https://vuejs.org/guide/components/v-model.html
const props = defineProps({
  modelValue: String
});

const emit = defineEmits([
  'update:modelValue',
]);

type ThemeOption = { id: string, value: string }

const themeOptions: ThemeOption[] = [
  'tomorrow_night_eighties',
  'chrome',
  'monokai',
].map(theme => {
  return {
    id: theme,
    value: theme,
  };
});

type LanguageOption = { id: string, value: string }

const languageOptions: LanguageOption[] = [
  'html',
  'javascript',
  'python',
].map(language => {
  return {
    id: language,
    value: language,
  };
});

const selected = reactive({
  language: languageOptions[0].id,
  theme: themeOptions[0].id,
});

// NOTE: Grabs the DOM element with `ref="codeEditor"`
const codeEditor = ref();

function getEditor(editor: Ref<any>) {
  return codeEditor.value._editor;
}

function updateContent(event: any) {
  emit('update:modelValue', getEditor(codeEditor).getValue());
}
</script>

<style scoped>
a {
  text-decoration: underline;
}
</style>