<template>
    <nav class="bg-richBlack">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div class="flex items-center justify-between h-16">
          <div class="flex-shrink-0">
            <a href="#" class="flex items-center">
              <span class="ml-2 p-2 rounded text-white text-xl transition duration-250 hover:text-white hover:bg-midnightGreen font-bold"><span class="text-tiffanyBlue">EK</span>.<strong>Edu</strong></span>
            </a>
          </div>
          <div class="hidden md:block">
            <div class="ml-4 flex items-center md:ml-6">
                <!-- TODO - write a loop and highlight active page; add Link instead -->
                <!-- TODO - fix hidden items, swap with hamburger menu & dropdown -->
              <a href="#" class="px-3 py-2 transition duration-250 rounded-md text-sm font-medium text-gray-300 hover:text-white hover:bg-midnightGreen bg-gray-800">Home</a>
              <a href="#" class="ml-4 px-3 transition duration-250 py-2 rounded-md text-sm font-medium text-gray-300 hover:text-white hover:bg-midnightGreen">About</a>
              <a href="#" class="ml-4 px-3 transition duration-250 py-2 rounded-md text-sm font-medium text-gray-300 hover:text-white hover:bg-midnightGreen">Contact</a>
            </div>
          </div>
        </div>
      </div>
    </nav>
</template>
  
<script lang="ts">
import Link from 'vue-router'
import { defineComponent } from 'vue';
// TODO - update a tags to links
export default defineComponent({
  name: 'Navbar',
});
</script>

