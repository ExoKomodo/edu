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
      v-model:value="editorContent"
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
import { reactive } from 'vue';

let editorContent: string = '<div>Hello world</div>';

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
</script>

<style scoped>
a {
  text-decoration: underline;
}
</style>