<template>
  <nav class="bg-richBlack">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
      <div class="flex items-center justify-between h-16">
        <div class="flex-shrink-0">
          <a href="/"
             class="flex items-center">
            <span class="ml-2 p-2 rounded text-white text-xl transition duration-250 hover:text-white hover:bg-midnightGreen font-bold">
              <span class="text-tiffanyBlue">EK</span>.<strong>Edu</strong>
            </span>
          </a>
        </div>
        <div class="hidden md:block">
          <div class="ml-4 flex items-center md:ml-6">
            <!-- TODO - fix hidden items, swap with hamburger menu & dropdown -->
            <RouterLink v-for="route in routes"
               :to=route.path
               class="px-3 m-2 py-2 transition duration-250 rounded-md text-sm font-medium text-gray-300 hover:text-white hover:bg-midnightGreen bg-gray-800">
              {{ route.name }}
            </RouterLink>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>
  
<script lang="ts" setup>
import { RouterLink } from 'vue-router';
import router from '@/router';

const routes = router.options.routes


</script>
