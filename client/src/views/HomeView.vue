<script setup lang="ts">
import InfoTile from '@/components/InfoTile.vue';
import Instructors from '@/components/Instructors.vue';
</script>

<template>
  <main class="mainBackground h-[92vh]">
    <div class="flex flex-row justify-center items-center">
        <div class="flex flex-col justify-center items-center h-screen">
          <div class="relative rounded-lg p-6 bg-opacity-25 backdrop-filter backdrop-blur-lg bg-white shadow-xl">
            <p class="p-1 text-2xl font-bold text-virgil drop-shadow-lg">Bing Bong.</p>
            <p class="p-1 text-virgil">Lorem, ipsum dolor sit amet consectetur adipisicing elit. Id, eius.</p>
          </div>
        </div>
    </div>
  </main>
  <div class="bg-richBlack h-10"></div>
  <section class="altBackground flex justify-center mb-5">
    <div class="container p-3 flex flex-row justify-center items-center">
      <InfoTile header="Learn stuff" />
      <InfoTile header="Tea is yummy"/>
    </div>
  </section>
  <div class="bg-richBlack h-10 py-5"></div>
  <Instructors />
  <div class="bg-richBlack h-10 py-5"></div>

</template>
