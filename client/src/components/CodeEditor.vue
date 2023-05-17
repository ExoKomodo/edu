<template>
    <label class="mr-2">Language:
      <select @change="onLanguage" v-model="selected.language" class="text-black">
        <option v-for="option in languageOptions" v-bind:value="option.id" :selected="selected.language == option.id">{{option.value}}</option>
      </select>
    </label>
    <label>Theme:
      <select @change="onTheme" v-model="selected.theme" class="text-black">
        <option v-for="option in themeOptions" :value="option.id" :selected="selected.theme == option.id">{{option.value}}</option>
      </select>
    </label>
    <v-ace-editor
      ref="codeEditor"
      :value="selected.content"
      @change="onContent"
      :lang="selected.language"
      :theme="selected.theme"
      :style="`height: ${height}rem`"
      :options="{
        useWorker: true,
        enableBasicAutocompletion: true,
        enableSnippets: true,
        enableLiveAutocompletion: true,
      }"/>
</template>

<script setup lang="ts">
import Constants from '@/services/Constants';
import Helpers from '@/services/Helpers';
import { onMounted, reactive, ref, type Ref } from 'vue';

// NOTE: Combination of props and emit helps set up `v-model`: https://vuejs.org/guide/components/v-model.html
const props = defineProps<{
  modelValue: string,
  height: number,
  theme?: string,
  language?: string,
}>();

const emit = defineEmits([
  'update:modelValue',
  'update:language',
  'update:theme',
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
  'plain_text',
  'html',
  'python',
  'javascript',
].map(language => {
  return {
    id: language,
    value: language,
  };
});

const selected = reactive({
  content: props.modelValue,
  language: !!props.language ? props.language : languageOptions[0].id,
  theme: !!props.theme ? props.theme : themeOptions[0].id,
});

// NOTE: Grabs the DOM element with `ref="codeEditor"`
const codeEditor = ref();

function onContent(event: any) {
  emit('update:modelValue', Helpers.getEditor(codeEditor).getValue());
}

function onLanguage(event: any) {
  emit('update:language', selected.language);
}

function onTheme(event: any) {
  emit('update:theme', selected.theme);
}

onMounted(() => {
  if (!props.modelValue) {
    Helpers.changeEditorContent(
      codeEditor,
      Constants.defaultLanguageContent(selected.language)
    );
  }
});
</script>

<style scoped>
a {
  text-decoration: underline;
}
</style>