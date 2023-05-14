<script setup lang="ts">
import InfoTile from '@/components/InfoTile.vue';
import Instructors from '@/components/Instructors.vue';
import CodeEditor from '@/components/CodeEditor.vue';

let editorContent = null;
</script>

<template>
  <main class="mainBackground md:h-[92vh]">
    <div class="flex flex-row justify-center items-center">
        <div class="flex flex-col justify-center items-center md:h-screen xs:h-4/5 xs:m-10">
          <div class="relative rounded-lg p-6 bg-opacity-25 backdrop-filter backdrop-blur-lg bg-white shadow-xl m-3">
            <p class="p-1 text-2xl font-bold text-virgil drop-shadow-lg">After the bootcamp.</p>
            <p class="p-1 text-virgil">Learn what wasn't taught to you. Upskill and grow.</p>
            <CodeEditor v-model="editorContent"></CodeEditor>
          </div>
        </div>
    </div>
  </main>
  <div class="bg-richBlack h-10"></div>
  <section class="altBackground flex justify-center pb-5">
    <div class="p-3 sm:flex-row xs:flex-row justify-center items-center">
      <InfoTile header="Advance your knowledge" />
      <InfoTile header="Strengthen your fundamentals"/>
    </div>
  </section>
  <div class="bg-richBlack h-10 py-5"></div>
  <Instructors />
  <div class="bg-richBlack h-10 py-5"></div>

</template>
