<template>
    <div v-for="post in finalData" :key="post._id" class="flex flex-col justify-center align-middle items-center bg-timberwolf m-3 p-3 shadow-lg rounded">
      <div class="flex flex-row text-2xl p-3 text-blue-600 border-b-slate-800 border-2 border-t-timberwolf border-x-timberwolf mb-5">
          <input class="m-3" type="checkbox" :value="post._id" v-model="selectedIds"/>
          <h2>{{ post.title }}</h2>
      </div>
      <p class="text-payne">{{ post.author }}</p>
      <p>{{ post.content.slice(0, 150).concat("...") }}</p>
      <p>Posted: {{ post.createdAt.slice(0,10) }}</p>
      <button class="p-3 bg-slate-800 text-white m-3 hover:bg-slate-100 transition duration-250 hover:text-slate-800" @click="deleteData">Delete</button>
    </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import { calls } from '../server_calls/calls';
const apiData: object = await calls.get()
const finalData: ApiPost[] = Object.values(apiData)
interface ApiPost {
    _id: string,
    title: string,
    author: string,
    content: string,
    createdAt: string,
    updatedAt: string,
}
const selectedIds = ref([])
const deleteData = () => {
    calls.delete(selectedIds.value[0])
}
</script>