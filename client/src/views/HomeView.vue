<template>
  <main class="mainBackground md:h-[92vh]">
    <div class="flex flex-row justify-center items-center">
        <div class="flex flex-col justify-center items-center md:h-screen xs:h-4/5 xs:m-10">
          <div class="relative rounded-lg p-6 bg-opacity-25 backdrop-filter backdrop-blur-lg bg-white shadow-xl m-3">
            <p class="p-1 text-2xl font-bold text-virgil drop-shadow-lg">After the bootcamp.</p>
            <p class="p-1 text-virgil">Learn what wasn't taught to you. Upskill and grow.</p>
            <CodeEditor v-on:update:language="onLanguageChange" ref="editor" v-model="pythonState.content" :language="pythonState.language" :theme="pythonState.theme" :height="20"></CodeEditor>
          </div>
        </div>
    </div>
  </main>
  <div class="bg-richBlack h-10"></div>
  <section class="altBackground flex justify-center pb-5">
    <div class="p-3 sm:flex-row xs:flex-row justify-center items-center">
      <InfoTile header="Advance your knowledge" :description=description />
      <InfoTile header="Strengthen your fundamentals" :description=altDescription />
    </div>
  </section>
  <div class="bg-richBlack h-10 py-5"></div>
  <Instructors />
  <div class="bg-richBlack h-10 py-5"></div>
  <footer class="bg-slate-700 p-5 flex justify-center text-virgil">&copy;2023 Exokomodo</footer>

</template>

<script setup lang="ts">
import InfoTile from '@/components/InfoTile.vue';
import Instructors from '@/components/Instructors.vue';
import CodeEditor from '@/components/CodeEditor.vue';
import { onMounted, reactive, ref, type Ref } from 'vue';
import Constants from '@/services/Constants';
import Helpers from '@/services/Helpers';

const description: string = `Learn more about coding with our highly qualified instructors. <a class="text-darkCyan" href="/">EK.edu</a> is oriented around
            bootcamp graduates, CS graduates, and others who are interested in becoming artisans of software. We aim to 
            take learners beyond the basics of how to write code, and focus on transforming into an artist of programming.`;
const altDescription: string = `Our classes come with years of real life experience and work in a variety of spaces including 
  small private businesses, enterprise companies, government contracts, freelance work, and more. Learn to problem solve in 
  language-agnostic ways and become a master of the full stack.`;

type CodeEditorModel = {
  content: string,
  language: string,
  theme: string,
};

const states: Map<string, CodeEditorModel> = new Map(
  Constants.languages.map(language => [
    language,
    reactive({
      content: '',
      language: language,
      theme: 'tomorrow_night_eighties',
    }),
  ])
);

const state = reactive({
  selected: states.get('python') as CodeEditorModel,
});

const pythonState = states.get('python') as CodeEditorModel;

const editor = ref();

function refreshEditorContent() {
  const codeEditor = editor.value.$refs.codeEditor as Ref<any>;
  Helpers.changeEditorContent(codeEditor, state.selected.content);
}

function onLanguageChange(language: string) {
  if (!(Constants.languages.includes(language))) {
    console.error(`Not a supported language: ${language}`);
    return;
  }
  state.selected = states.get(language) as CodeEditorModel;
  if (!state.selected.content) {
    state.selected.content = Constants.defaultLanguageContent(state.selected.language);
  }
  refreshEditorContent();
}

onMounted(() => {
  refreshEditorContent();
});
</script>
