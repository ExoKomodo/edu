<template>
    <div class="flex flex-col p-5 items-center justify-center">
      <div class="bg-slate-400 p-5 ">
        <form class="p-7 flex flex-col w-full justify-center items-center align-top">
          <div class="w-full">
            <label>
              <h3 class="text-xl py-2">Title:</h3>
              <input class="w-full" v-model="title" type="text" />
            </label>
          </div>
          <div class="w-full">
            <label>
              <h3 class="text-xl py-2">Author:</h3>
              <input class="w-full" v-model="author" type="text" />
            </label>
          </div>
          <div class="w-full">
            <label>
              <h3 class="text-xl py-2">Content:</h3>
              <textarea class="w-full" v-model="content"></textarea>
            </label>
          </div>
          <div>
            <button class="p-3 bg-slate-800 text-white m-3 hover:bg-slate-100 transition duration-250 hover:text-slate-800" @click.prevent="handleSubmit">Submit</button>
            <button class="p-3 bg-slate-800 text-white m-3 hover:bg-slate-100 transition duration-250 hover:text-slate-800" @click.prevent="getData">Get data</button>
          </div>
        </form>
        <Suspense>
          <DataGrid />
        </Suspense>
      </div>
    </div>
  </template>
  
  <script lang="ts" setup>
  import { ref } from 'vue';
  import DataGrid from '../components/DataGrid.vue';
  import { calls } from '../server_calls/calls'
  const title = ref('');
  const author = ref('');
  const content = ref('');
  
  const getData: () => void = async () => console.log(await calls.get())
  
  function handleSubmit() {
    const formData = {
      title: title.value,
      author: author.value,
      content: content.value,
    };
    console.log(formData); 
    calls.post(formData);
    }
  
  </script>